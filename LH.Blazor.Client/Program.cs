using Package.LH.BlazorComponents.DependencyInjection;
using Package.LH.Services.Configurations;
using Package.LH.Services.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Package.Shared.BlazorComponents;
using Package.Shared.Entities.VCBInterfaces;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.DependencyInjection;
using Package.Shared.Services.StateServices.AttendeeStateServices;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("LHAPIServiceHttpClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:44326/");
});


builder.Services.AddSingleton<IJSEnabled>(sp =>
{
    return new JSEnabled
    {
        JSIsEnabled = true, //if we are inject the client then it is true
        TestingWhoAmI = "Client"
    };
});
//below works but want to try doing it via server program.cs

//!!Leave in tested in release mode but based on stack overflow expect publishing issues tree shaking again - though expect better solution is the assemblies creation in server proj!!

builder.Services.RegisterAllGenericBlazorComponents();
builder.Services.RegisterAllLHBazorComponents();


// Add Configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



// Use the extension method to configure services from the shared package
builder.Services.AddConfigurationFromSharedServicesPackage(builder.Configuration);
builder.Services.AddStateServicesFromSharedServicesPackage();

builder.Services.AddConfigurationFromLHServicesPackage(builder.Configuration);
builder.Services.AddStateServicesFromLHServicesPackage();

await builder.Build().RunAsync();
