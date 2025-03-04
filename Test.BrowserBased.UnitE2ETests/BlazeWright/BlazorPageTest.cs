using Microsoft.AspNetCore.Hosting;
using Microsoft.Playwright.Xunit;
using Microsoft.Playwright;
using System.Threading.Tasks;
using Test.BrowserBased.UnitE2ETests.Helpers;


namespace Test.BrowserBased.UnitE2ETests.BlazeWright;


public class BlazorPageTest<TProgram> : PageTest, IAsyncLifetime
    where TProgram : class
{
    protected string BaseUrl => Host.ServerAddress;
    private BlazorApplicationFactory<TProgram>? host;

    protected BlazorApplicationFactory<TProgram> Host
    {
        get
        {
            host ??= CreateHostFactory() ?? new BlazorApplicationFactory<TProgram>(ConfigureWebHost);
            return host;
        }
    }

    protected virtual BlazorApplicationFactory<TProgram> CreateHostFactory()
        => new BlazorApplicationFactory<TProgram>(ConfigureWebHost);

    protected virtual void ConfigureWebHost(IWebHostBuilder builder) { }

}
