using Package.LH.BlazorComponents.DependencyInjection;
using LH.MVCBlazor.Server.BlazorPageHosting;
using LH.MVCBlazor.Server.Middleware.LH.MVCBlazor.Server.Middleware;
using Package.LH.Services.Configurations;
using Package.LH.Services.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using LH.MVCBlazor.Server.Middleware;
using Package.Shared.BlazorComponents.DependencyInjection;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.DependencyInjection;
using Microsoft.AspNetCore.Components;

using Serilog;
using Serilog.Events;
using Serilog.Core;
using Package.Shared.Services.HelperServices.LogLevelSwitcherService;
using Microsoft.Extensions.Logging;

//using Serilog.Templates;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Logging.ClearProviders();


// Read default logging level from configuration
var logLevelString = builder.Configuration["Serilog:MinimumLevel:Default"];

// Convert string to LogEventLevel (with fallback)
if (!Enum.TryParse(logLevelString, true, out LogEventLevel defaultLogLevel))
{
    defaultLogLevel = LogEventLevel.Information; // Default if parsing fails
}


// Create a LoggingLevelSwitch that can be updated dynamically
LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(defaultLogLevel); // Default: Information added this so in production can change the logging




Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.ControlledBy(levelSwitch)
    .CreateLogger();
/*
    Serverside is the prerender
    It wont browserConsole log
    We can instead have a logging service and hit an api if we want clientside 
    browser logs from wasm and server side logs centralised
 */


// Add Serilog to logging providers
builder.Logging.AddSerilog(Log.Logger, dispose: true);
builder.Host.UseSerilog();



// Register the LoggingLevelSwitch in DI container

// Capture big failures
try
{
    
    builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddCircuitOptions(opt => opt.DetailedErrors = true)
                .AddInteractiveWebAssemblyComponents();


    // Add services to the container.
    builder.Services.AddControllersWithViews();

    string LH_DB_API_BaseURL;
    string LH_DB_API_ClientName;

    try
    {
        LH_DB_API_BaseURL = builder.Configuration["APIs:LH_DB_API:BaseURL"];
        LH_DB_API_ClientName = builder.Configuration["APIs:LH_DB_API:ClientName"];
    }
    catch (Exception e)
    {
        // Log or handle the error appropriately
        // Its probably missing appsetting info
        Console.WriteLine($"Configuration validation failed: {e.Message}");
        throw; // Re-throw if necessary

    }

    builder.Services.AddHttpClient(LH_DB_API_ClientName, client =>
    {
        client.BaseAddress = new Uri(LH_DB_API_BaseURL);
    });


    builder.Services.AddHttpContextAccessor();

    //Want to pass a configuration that is a configuration just of what is relevant to the package. And interface to error in the server by not fitting appsettings.
    builder.Services.LHS_AddConfiguration(builder.Configuration,"APIs:LH_DB_API");
    builder.Services.LHS_AddStateServices();

    builder.Services.GS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
    builder.Services.GS_AddStateServices();
    builder.Services.LHB_RegisterAllBlazorPageRoutes();

    builder.Services.AddSession(options =>
    {

        options.IdleTimeout = TimeSpan.FromMinutes(20); // Set the timeout for the session
        options.Cookie.HttpOnly = true; // Session cookie is only accessible via HTTP
        options.Cookie.IsEssential = true; // Session cookie is essential for application
    });





    builder.Services.AddScoped<IGS_JSEnabled>(provider =>
    {
        //In here we would get our appsettings etc and configure - but then we have an object to pass it 
        var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        //For this to work well for blazor pages as well we would probably want to set up session storage (which takes js anyway to set up and most blazor application use it i presume)
        var session = httpContextAccessor.HttpContext?.Session;
        bool JSEnabled = session.GetString("JsEnabled") == "true";

        return new GS_JSEnabled
        {
            JSIsEnabled = JSEnabled,
            TestingWhoAmI = "Server"
        };
    });
    //Scoped because being consumed with storage where singleton doesnt survive mvc page teardown
    builder.Services.AddScoped<LoggingLevelSwitch>(sp=>levelSwitch);
    builder.Services.AddScoped<ILogLevelSwitcherService, SerilogLogLevelSwitcherService>();
    var app = builder.Build();

    app.UseSession(); //session so we can track if noJs browser
    app.UseMiddleware<JsEnabledBrowserDetectionMiddleware>();


    app.UseSerilogRequestLogging();


    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        app.UseWebAssemblyDebugging();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();


    app.UseRouting();

    app.UseAuthorization();
    app.UseAntiforgery(); 

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //if using interactive
    //https://learn.microsoft.com/en-us/aspnet/core/blazor/components/integration?view=aspnetcore-8.0#use-non-routable-components-in-pages-or-views
    app.MapRazorComponents<App>() 
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(Package.LH.BlazorComponents._Imports).Assembly);

    app.MapFallbackToController("Blazor", "Home"); 

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensure logs are flushed before exit
}

public partial class Program { } //atoz blazor playwright example uses this for the inmemory testing