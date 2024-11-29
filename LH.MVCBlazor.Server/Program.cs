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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddCircuitOptions(opt => opt.DetailedErrors = true) //commenting out due to debugger error
                .AddInteractiveWebAssemblyComponents();


// .AddInteractiveWebAssemblyRenderMode(); // Blazor WebAssembly may want this


// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient("LHAPIServiceHttpClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:44306/");//API SSL
//});


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


// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

//Want to pass a configuration that is a configuration just of what is relevant to the package. And interface to error in the server by not fitting appsettings.
builder.Services.LHS_AddConfiguration(builder.Configuration,"APIs:LH_DB_API");
builder.Services.LHS_AddStateServices();

builder.Services.GS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API");
builder.Services.GS_AddStateServices();


builder.Services.LHB_RegisterAllBlazorComponents();
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

//qqqq wish it could be singleton
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
app.UseAntiforgery(); //qqq-FromBlazorWebAppTemplate

//qqqq hmm no useblazorframeworkfiles here wasnt in the microsoft talk
//app.UseBlazorFrameworkFiles(); //qqqq just trying it
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapBlazorHub();//there isnt here the interactive webapp stuff either qqqq so expecting
//it will get complicated later - this is suppost to be better with cshtml without changing index page
//if using server blazor script

//app.UseBlazorFrameworkFiles(); //if using wasm script js
//this is what we have in mvcwasmnuget app.maprazorcomponents<app> is what is in the microsoft docs but theyre not for mvc set up

//if using interactive
//https://learn.microsoft.com/en-us/aspnet/core/blazor/components/integration?view=aspnetcore-8.0#use-non-routable-components-in-pages-or-views
app.MapRazorComponents<App>() //The microsoft video though said .net 8 was .net 7 approach and webserver to dodge the issue of non blazor routed projects but here we have a special App.razor just with a comment from 
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Package.LH.BlazorComponents.Components.Pages.RenderModePages.CharactersRenderModePages.WebAssemblyCharactersPage).Assembly);
//Leave this in too for reference!!!
// , prerendering: false); we can add this to the end if wanting to globally disable prerendering. Though lets leave it in unless its a problem.

//added to try and enable blazor pages to be in a package
//!!Leave in tested in release mode but based on stack overflow expect publishing issues tree shaking again!!
//  .AddAdditionalAssemblies(typeof(MVCWebApp.BlazorWasmClient._Imports).Assembly); //qqqq this should be explored may be how we get access to all packages

// Keeping this i can have fallback in App logic but this lets MVC handle it
// **(q)** qqqq but what is blazor got to do with it it could be any failed route
app.MapFallbackToController("Blazor", "Home"); /*.net 8 is thos still doing something useful? qqqq*/

app.Run();
