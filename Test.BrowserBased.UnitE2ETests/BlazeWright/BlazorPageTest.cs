using Microsoft.AspNetCore.Hosting;
using Microsoft.Playwright.Xunit;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Test.BrowserBased.UnitE2ETests.BlazeWright;

public class BlazorPageTest<TProgram> : PageTest, IAsyncLifetime
    where TProgram : class
{
    /// <summary>
    /// Being able to reuse them is not necessary it was added
    /// qqqq not using these now should we?
    /// </summary>
    //protected IBrowserContext? BrowserContextWithJS;
    //protected IBrowserContext? BrowserContextWithoutJS;

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

    public BlazorPageTest() { }


    //public override BrowserNewContextOptions ContextOptions()
    //{
    //    var options = base.ContextOptions() ?? new BrowserNewContextOptions();
    //    options.BaseURL = Host.ServerAddress;
    //    options.IgnoreHTTPSErrors = true;
    //    return options;
    //}

    //public BrowserNewContextOptions ContextOptionsWithoutJS()
    //{
    //    var options = ContextOptions();
    //    options.JavaScriptEnabled = false;
    //    return options;
    //}

    //public async Task InitializeAsync()
    //{
    //    await base.InitializeAsync();
    //    //BrowserContextWithJS = await Browser.NewContextAsync(ContextOptions());
    //    //BrowserContextWithoutJS = await Browser.NewContextAsync(ContextOptionsWithoutJS());
    //}

    //public new async Task DisposeAsync()
    //{
    //    if (host is { } currentHost)
    //    {
    //        host = null;
    //        await base.DisposeAsync();
    //        await currentHost.DisposeAsync();
    //    }
    //}
}
