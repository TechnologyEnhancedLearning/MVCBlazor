using Package.LH.Services.Configurations;
using Package.LH.Services.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Package.Shared.BlazorComponents;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.DependencyInjection;
using Package.Shared.Services.StateServices;
using Package.Shared.BlazorComponents.DependencyInjection;
using Package.LH.BlazorComponents.DependencyInjection;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using Blazored.LocalStorage;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using Serilog.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Package.Shared.Services.HelperServices.LogLevelSwitcherService;
using Microsoft.Extensions.DependencyInjection;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add Configuration from appsettings.json
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

// Add Serilog to logging providers
builder.Logging.AddSerilog(Log.Logger, dispose: true);



//for really bad fails
try { 

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
        //If this is in production and this error is to do with configuration that we may never get it as will be relying on the api, which comes from the configuration
        Log.Error(e, "Configuration validation failed");
        throw; 

    }

    builder.Services.AddHttpClient(LH_DB_API_ClientName, client =>
    {
        client.BaseAddress = new Uri(LH_DB_API_BaseURL);
    });



    builder.Services.AddSingleton<IGS_JSEnabled>(sp =>
    {
        return new GS_JSEnabled
        {
            JSIsEnabled = true, //if we are inject the client then it is true
            TestingWhoAmI = "Client"
        };
    });


    // Add Configurations and Services
    builder.Services.LHS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
    builder.Services.LHS_AddStateServices();

    builder.Services.GS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
    builder.Services.GS_AddStateServices();

    builder.Services.LHB_RegisterAllBlazorPageRoutes();

    //Scoped because being consumed with storage where singleton doesnt survive mvc page teardown
    builder.Services.AddScoped<LoggingLevelSwitch>(sp=>levelSwitch);
    builder.Services.AddScoped<ILogLevelSwitcherService, SerilogLogLevelSwitcherService>();
    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    //If in production as requires sending to api we may never receive it
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensure logs are flushed before exit
}