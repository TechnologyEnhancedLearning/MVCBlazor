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
//below works but want to try doing it via server program.cs

//!!Leave in tested in release mode but based on stack overflow expect publishing issues tree shaking again - though expect better solution is the assemblies creation in server proj!!

builder.Services.GB_RegisterAllGenericBlazorComponents();

builder.Services.LHB_RegisterAllBlazorComponents();
builder.Services.LHB_RegisterAllBlazorPageRoutes();


// Add Configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



// Use the extension method to configure services from the shared package
builder.Services.GS_AddConfiguration(builder.Configuration,"APIs:LH_DB_API");
builder.Services.GS_AddStateServices();


builder.Services.LHS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
builder.Services.LHS_AddStateServices();

await builder.Build().RunAsync();
