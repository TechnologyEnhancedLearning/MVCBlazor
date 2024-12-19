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

var builder = WebApplication.CreateBuilder(args);
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
    //Not necessary, but for efficiency I want to know when staticly rendering whether to use the no js version so we need a session as 
    // the blazor will run staticly first before any checks it can run on the browser for the js. i dont want to render the noJs version if its not needed
    //so lets find out if it is and then store it - there is the slight disadavantage of not checking per page or per component that turning js back on may not update the value but its out of scope

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


var app = builder.Build();

app.UseSession(); //session so we can track if noJs browser
app.UseMiddleware<JsEnabledBrowserDetectionMiddleware>();

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
