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



var builder = WebAssemblyHostBuilder.CreateDefault(args);
//move up qqqq
// Add Configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
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
    // Its probably missing appsetting info
    Console.WriteLine($"Configuration validation failed: {e.Message}");
    throw; // Re-throw if necessary

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



builder.Services.LHS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
builder.Services.LHS_AddStateServices();

builder.Services.GS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
builder.Services.GS_AddStateServices();



builder.Services.LHB_RegisterAllBlazorPageRoutes();

//move up qqqq
// Add Configuration from appsettings.json
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


await builder.Build().RunAsync();
}
catch (Exception ex)
{
    // qqqqqq but where would this log to ... we need a service and an api post... but that may rely on everything else having succeeded
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensure logs are flushed before exit
}