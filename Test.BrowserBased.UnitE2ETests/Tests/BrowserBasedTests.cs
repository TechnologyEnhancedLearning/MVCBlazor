using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.BrowserBased.Host;


using static Microsoft.Playwright.Assertions;
using Test.BrowserBased.UnitE2ETests.Helpers;


namespace Test.BrowserBased.UnitE2ETests.Tests
{
    /// <summary>
    /// Blazor Page Test puts boiler place behind the tests
    /// </summary>
    public class BrowserBasedTests : BlazorPageTest<Program>
    {
        [Fact]
        public async Task ProductionPlaywrightSiteIsReachableAndHasTitle()
        {
            await Page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
        }




        [Fact]
        public async Task MainNavigation()//qqqq this works atleast
        {
            // var playwright = await Playwright();
            var browser = await Playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev");
            Assert.Equal("https://playwright.dev/", page.Url);
        }





        [Theory]

        [InlineData("chromium", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("chromium", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("firefox", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("firefox", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("webkit", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("webkit", false, ViewportHelper.ViewportType.Mobile)]
        public async Task Page_Loads_Correctly(string browserType, bool jsEnabled, ViewportHelper.ViewportType viewport)
        {
            
            string baseUrl = Host.ServerAddress;

            //Handling this per test and running them all on the browser is quick for writing tests slow for running test
            //alternatively we could have a browser per test file
            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            //qqqq ussually we will use it like this? probs
            //IPage page = await (await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, baseUrl)).NewPageAsync();
           
            IBrowserContext browserContext = await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, baseUrl);
            //Debug option
            await browserContext.Tracing.StartAsync(new()
            {

                Screenshots = true,
                Snapshots = true,
                Sources = true
            });


            IPage page = await browserContext.NewPageAsync();

            //Debug option
            //await page.PauseAsync();

            //await page.GotoPreRenderedAsync("counter");
            await page.GotoAsync("counter", new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });
            ILocator status = page.GetByRole(AriaRole.Status);
            await Expect(status).ToHaveTextAsync("Current count: 0");

            await browserContext.Tracing.StopAsync(new()
            {
                Path = "trace.zip",
            });

        }

        //qqqqqq counter test nojs
        //qqqqqq page for prerender server wasm and no prerender

        [Theory]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("chromium", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("firefox", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("firefox", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("webkit", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("webkit", false, ViewportHelper.ViewportType.Mobile)]
        public async Task Page_InteractivityIsCorrectlySimulated(string browserType, bool jsEnabled, ViewportHelper.ViewportType viewport)
        {

            string baseUrl = Host.ServerAddress;

            //Handling this per test and running them all on the browser is quick for writing tests slow for running test
            //alternatively we could have a browser per test file
            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            //qqqq ussually we will use it like this? probs
            //IPage page = await (await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, baseUrl)).NewPageAsync();

            IBrowserContext browserContext = await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, baseUrl);
            //Debug option
            await browserContext.Tracing.StartAsync(new()
            {

                Screenshots = true,
                Snapshots = true,
                Sources = true
            });


            IPage page = await browserContext.NewPageAsync();

            //Debug option
            await page.PauseAsync();

            //await page.GotoPreRenderedAsync("counter");
            await page.GotoAsync("counter", new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });
            ILocator status = page.GetByRole(AriaRole.Status);
            await Expect(status).ToHaveTextAsync("Current count: 0");

            // Find the button and click it
            ILocator button = page.GetByRole(AriaRole.Button, new() { Name = "Click me" }); // Change the name if needed
            await button.ClickAsync();

            // Check if JavaScript is enabled by verifying the text changes
            await Expect(status).ToHaveTextAsync(jsEnabled ? "Current count: 1" : "Current count: 0");

            await browserContext.Tracing.StopAsync(new()
            {
                Path = "trace.zip",
            });

        }


        /// <summary>
        /// Manual set up for customising
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Count_Increments_WhenButtonIsClicked_ManualSetUp()//works qqqq why different from other
        {
            // Arrange
            // Runs Blazor App referenced by Program, making it
            // available on 127.0.0.1 on a random free port.
            using var host = new BlazorApplicationFactory<Program>(); // Replace with your Program class

            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using IBrowser? browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }); // Headless set to false to see the browser
            BrowserNewContextOptions contextOptions = new BrowserNewContextOptions
            {
                // Assigns the base address of the host
                // (cannot be hardcoded due to random chosen port)
                BaseURL = host.ServerAddress,
                // BAF/WAF uses dotnet dev-cert for HTTPS. If
                // that is not trusted on your CI pipeline, this ensures
                // that tests will continue working.
                IgnoreHTTPSErrors = true,
            };
            await Page.PauseAsync();
            await Context.Tracing.StartAsync(new()
            {

                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
            IBrowserContext context = await browser.NewContextAsync(contextOptions);
            IPage page = await context.NewPageAsync();

            // Go to the counter page, and waits till the network is idle.
            // This is needed when pre-rendering is enabled and using Blazor Server,
            // since the page is not interactive until the SignalR connection to the
            // backend has been established.
            await page.GotoAsync(
                "counter",
                new PageGotoOptions()
                {
                    WaitUntil = WaitUntilState.NetworkIdle
                });

            // Act
            await page
                .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
                .ClickAsync();

            // Assert
            ILocator status = page.GetByRole(AriaRole.Status);
            await Expect(status).ToHaveTextAsync("Current count: 1");
            await Context.Tracing.StopAsync(new()
            {

                Path = "trace.zip",
            });
        }
    }
    //[Fact]
    //public async Task Count_Increments_WhenButtonIsClicked()
    //{
    //    await Page.PauseAsync();
    //    await Context.Tracing.StartAsync(new()
    //    {

    //        Screenshots = true,
    //        Snapshots = true,
    //        Sources = true
    //    });

    //    //qqqq dont i need this still
    //    // I want to be able to run for each browser. qqqq
    //    //var browser = await Playwright.Chromium.LaunchAsync();
    //    //var page = await browser.NewPageAsync();

    //    // Now we can use relative paths since base URL is set
    //    await Page.GotoAsync("/");

    //    await Page.GetByRole(AriaRole.Link, new() { Name = "Counter" }).ClickAsync();
    //    await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
    //    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 1");

    //    await Context.Tracing.StopAsync(new()
    //    {

    //        Path = "trace.zip",
    //    });

    //    //  using BlazorApplicationFactory<Program> host = new BlazorApplicationFactory<Program>();

    //    //await Page.PauseAsync();
    //    //await Context.Tracing.StartAsync(new()
    //    //{

    //    //    Screenshots = true,
    //    //    Snapshots = true,
    //    //    Sources = true
    //    //});

    //    //await Page.GotoAsync(RootUri.AbsoluteUri);//qqqq come back to this 


    //    ////var playwright = await Playwright.CreateAsync();
    //    //var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    //    //{
    //    //    // Ignore HTTPS errors if you're using a self-signed certificate
    //    //    IgnoreHTTPSErrors = true
    //    //});

    //    /*    Microsoft.Playwright.PlaywrightException : net::ERR_CONNECTION_REFUSED at https://localhost:44336/
    //        Call log:
    //          -   - navigating to "https://localhost:44336/", waiting until "load"

    //          Stack Trace: 
    //        Connection.InnerSendMessageToServerAsync[T](ChannelOwner object, String method, Dictionary`2 dictionary, Boolean keepNulls)
    //        Connection.WrapApiCallAsync[T](Func`1 action, Boolean isInternal)
    //        Frame.GotoAsync(String url, FrameGotoOptions options)
    //        UnitTest1.Count_Increments_WhenButtonIsClicked() line 62
    //        --- End of stack trace from previous location ---
    //    */

    //    //await Page.GotoAsync("https://localhost:44336/");

    //    //await Page.GetByRole(AriaRole.Link, new() { Name = "Counter" }).ClickAsync();

    //    //await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();

    //    //await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 1");

    //    //await Context.Tracing.StopAsync(new()
    //    //{

    //    //    Path = "trace.zip",
    //    //});
    //}
    //qqqqqqqqqqqqqqqqq yeah just follow how eagel does it
    //
    //public class PlaywrightTests : IClassFixture<WebApplicationFactory<YourStartupClass>>
    //{
    //    private readonly WebApplicationFactory<YourStartupClass> _factory;
    //    private readonly string _baseUrl;

    //    public PlaywrightTests(WebApplicationFactory<YourStartupClass> factory)
    //    {
    //        _factory = factory;
    //        _baseUrl = _factory.Server.BaseAddress.ToString();
    //    }




    //[Fact]
    //public async Task HitLocalhost()
    //{
    //    using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
    //    await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    //    {
    //        Headless = false
    //    });
    //    var browser = await playwright.Chromium.LaunchAsync();
    //    var page = await browser.NewPageAsync();

    //    await page.GotoAsync("http://localhost:44336");

    //    Assert.True(await page.ContentAsync() != null);

    //    await page.CloseAsync();
    //    await browser.CloseAsync();
    //}

}