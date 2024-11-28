using Package.LH.Services.Configurations;
using Package.LH.Services.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Package.Shared.BlazorComponents;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.DependencyInjection;
using Package.Shared.Services.StateServices;
using Package.Shared.BlazorComponents.DependencyInjection;
using Package.LH.BlazorComponents.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Read the API Base URL from configuration
var apiBaseUrl = builder.Configuration["LH_DB_API:BaseUrl"];

// Configure HttpClient to use the base URL from the configuration
builder.Services.AddHttpClient("LHDBAPIServiceHttpClient", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl); // Use BaseUrl from appsettings.json
});


builder.Services.AddSingleton<IGS_JSEnabled>(sp =>
{
    return new GS_JSEnabled
    {
        JSIsEnabled = true, //if we are inject the client then it is true
        TestingWhoAmI = "Client"
    };
});
//below works but want to try doing it via server program.cs

//!!Leave in tested in release mode but based on stack overflow expect publishing issues tree shaking again - though expect better solution is the assemblies creation in server proj!!

builder.Services.GB_RegisterAllGenericBlazorComponents();

builder.Services.LHB_RegisterAllBlazorComponents();
builder.Services.LHB_RegisterAllBlazorPageRoutes();


// Add Configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



// Use the extension method to configure services from the shared package
builder.Services.GS_AddConfiguration(builder.Configuration);
builder.Services.GS_AddStateServices();

///qqqq put back in
//builder.Services.LHS_AddConfiguration(builder.Configuration);
//builder.Services.LHS_AddStateServices();

await builder.Build().RunAsync();
