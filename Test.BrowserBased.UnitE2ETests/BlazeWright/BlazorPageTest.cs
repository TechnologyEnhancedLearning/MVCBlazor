using Microsoft.AspNetCore.Hosting;
using Microsoft.Playwright.Xunit;
using Microsoft.Playwright;

namespace Test.BrowserBased.UnitE2ETests.BlazeWright;

public class BlazorPageTest<TProgram> : PageTest, IAsyncLifetime
    where TProgram : class
{

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

    public override BrowserNewContextOptions ContextOptions()
    {
        var options = base.ContextOptions() ?? new BrowserNewContextOptions();
        options.BaseURL = Host.ServerAddress;
        options.IgnoreHTTPSErrors = true;
        return options;
    }

    public async Task InitializeAsync()
    {
        await base.InitializeAsync();
    }

    public new async Task DisposeAsync()
    {
        if (host is { } currentHost)
        {
            host = null;
            await base.DisposeAsync();
            await currentHost.DisposeAsync();
        }
    }
}
