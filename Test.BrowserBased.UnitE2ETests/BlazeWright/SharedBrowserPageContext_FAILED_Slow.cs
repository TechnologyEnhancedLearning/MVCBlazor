using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.BrowserBased.UnitE2ETests.Helpers;

namespace Test.BrowserBased.UnitE2ETests.BlazeWright
{
    public class SharedBrowserPageContext_FAILED_Slow
    {
//        using Microsoft.AspNetCore.Hosting;
//using Microsoft.Playwright.Xunit;
//using Microsoft.Playwright;
//using System.Threading.Tasks;
//using Test.BrowserBased.UnitE2ETests.Helpers;


//namespace Test.BrowserBased.UnitE2ETests.BlazeWright;

//    
//    public class BlazorPageTest<TProgram> : PageTest, IAsyncLifetime
//        where TProgram : class
//    {
//        protected IPlaywright Playwright { get; private set; }
//        private BlazorApplicationFactory<TProgram>? host;
//        private bool enableTracing = true;
//        protected BlazorApplicationFactory<TProgram> Host
//        {
//            get
//            {
//                host ??= CreateHostFactory() ?? new BlazorApplicationFactory<TProgram>(ConfigureWebHost);
//                return host;
//            }
//        }

//        protected virtual BlazorApplicationFactory<TProgram> CreateHostFactory()
//            => new BlazorApplicationFactory<TProgram>(ConfigureWebHost);

//        protected virtual void ConfigureWebHost(IWebHostBuilder builder) { }

//        // Enum for browser configurations
//        public enum BrowserConfig
//        {
//            ChromiumDesktopJS,
//            ChromiumDesktopNoJS,
//            ChromiumMobileJS,
//            ChromiumMobileNoJS,
//            FirefoxDesktopJS,
//            FirefoxDesktopNoJS,
//            FirefoxMobileJS,
//            FirefoxMobileNoJS,
//            WebkitDesktopJS,
//            WebkitDesktopNoJS,
//            WebkitMobileJS,
//            WebkitMobileNoJS
//        }

//        // Dictionary to store browser contexts
//        protected Dictionary<BrowserConfig, IBrowserContext> BrowserContexts { get; private set; }
//        protected Dictionary<BrowserConfig, IPage> BrowserContextPageDictionary { get; private set; }

//        public BlazorPageTest()
//        {
//            // These will be initialized in InitializeAsync
//            BrowserContexts = new Dictionary<BrowserConfig, IBrowserContext>();
//            BrowserContextPageDictionary = new Dictionary<BrowserConfig, IPage>();



//        }

//        //This could be sped up with await Task whenall and other parralism options if needed
//        public async Task InitializeAsync()
//        {
//            // Initialize Playwright
//            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

//            // Initialize host to get base URL
//            string baseUrl = Host.ServerAddress;

//            // Initialize browsers
//            await InitializeBrowsersAsync(baseUrl);

//        }
//        private async Task InitializeBrowsersAsync(string baseUrl)
//        {

//            IBrowserContext ChromiumDesktopJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "chromium", true, ViewportHelper.ViewportType.Desktop, baseUrl, enableTracing);
//            IBrowserContext ChromiumDesktopNoJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "chromium", false, ViewportHelper.ViewportType.Desktop, baseUrl, enableTracing);
//            IBrowserContext ChromiumMobileJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "chromium", true, ViewportHelper.ViewportType.Mobile, baseUrl, enableTracing);
//            IBrowserContext ChromiumMobileNoJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "chromium", false, ViewportHelper.ViewportType.Mobile, baseUrl, enableTracing);
//            IBrowserContext FirefoxDesktopJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "firefox", true, ViewportHelper.ViewportType.Desktop, baseUrl, enableTracing);
//            IBrowserContext FirefoxDesktopNoJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "firefox", false, ViewportHelper.ViewportType.Desktop, baseUrl, enableTracing);
//            IBrowserContext FirefoxMobileJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "firefox", true, ViewportHelper.ViewportType.Mobile, baseUrl, enableTracing);
//            IBrowserContext FirefoxMobileNoJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "firefox", false, ViewportHelper.ViewportType.Mobile, baseUrl, enableTracing);
//            IBrowserContext WebkitDesktopJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "webkit", true, ViewportHelper.ViewportType.Desktop, baseUrl, enableTracing);
//            IBrowserContext WebkitDesktopNoJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "webkit", false, ViewportHelper.ViewportType.Desktop, baseUrl, enableTracing);
//            IBrowserContext WebkitMobileJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "webkit", true, ViewportHelper.ViewportType.Mobile, baseUrl, enableTracing);
//            IBrowserContext WebkitMobileNoJSBrowserContext = await Helpers.BrowserHelper.CreateBrowserContextAsync(Playwright, "webkit", false, ViewportHelper.ViewportType.Mobile, baseUrl, enableTracing);


//            BrowserContexts.Add(BrowserConfig.ChromiumDesktopJS, ChromiumDesktopJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.ChromiumDesktopNoJS, ChromiumDesktopNoJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.ChromiumMobileJS, ChromiumMobileJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.ChromiumMobileNoJS, ChromiumMobileNoJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.FirefoxDesktopJS, FirefoxDesktopJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.FirefoxDesktopNoJS, FirefoxDesktopNoJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.FirefoxMobileJS, FirefoxMobileJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.FirefoxMobileNoJS, FirefoxMobileNoJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.WebkitDesktopJS, WebkitDesktopJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.WebkitDesktopNoJS, WebkitDesktopNoJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.WebkitMobileJS, WebkitMobileJSBrowserContext);
//            BrowserContexts.Add(BrowserConfig.WebkitMobileNoJS, WebkitMobileNoJSBrowserContext);

//            IPage ChromiumDesktopJSPage = await ChromiumDesktopJSBrowserContext.NewPageAsync();
//            IPage ChromiumDesktopNoJSPage = await ChromiumDesktopNoJSBrowserContext.NewPageAsync();
//            IPage ChromiumMobileJSPage = await ChromiumMobileJSBrowserContext.NewPageAsync();
//            IPage ChromiumMobileNoJSPage = await ChromiumMobileNoJSBrowserContext.NewPageAsync();
//            IPage FirefoxDesktopJSPage = await FirefoxDesktopJSBrowserContext.NewPageAsync();
//            IPage FirefoxDesktopNoJSPage = await FirefoxDesktopNoJSBrowserContext.NewPageAsync();
//            IPage FirefoxMobileJSPage = await FirefoxMobileJSBrowserContext.NewPageAsync();
//            IPage FirefoxMobileNoJSPage = await FirefoxMobileNoJSBrowserContext.NewPageAsync();
//            IPage WebkitDesktopJSPage = await WebkitDesktopJSBrowserContext.NewPageAsync();
//            IPage WebkitDesktopNoJSPage = await WebkitDesktopNoJSBrowserContext.NewPageAsync();
//            IPage WebkitMobileJSPage = await WebkitMobileJSBrowserContext.NewPageAsync();
//            IPage WebkitMobileNoJSPage = await WebkitMobileNoJSBrowserContext.NewPageAsync();

//            BrowserContextPageDictionary.Add(BrowserConfig.ChromiumDesktopJS, ChromiumDesktopJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.ChromiumDesktopNoJS, ChromiumDesktopNoJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.ChromiumMobileJS, ChromiumMobileJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.ChromiumMobileNoJS, ChromiumMobileNoJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.FirefoxDesktopJS, FirefoxDesktopJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.FirefoxDesktopNoJS, FirefoxDesktopNoJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.FirefoxMobileJS, FirefoxMobileJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.FirefoxMobileNoJS, FirefoxMobileNoJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.WebkitDesktopJS, WebkitDesktopJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.WebkitDesktopNoJS, WebkitDesktopNoJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.WebkitMobileJS, WebkitMobileJSPage);
//            BrowserContextPageDictionary.Add(BrowserConfig.WebkitMobileNoJS, WebkitMobileNoJSPage);

//        }

//        public override async Task DisposeAsync()
//        {
//            foreach (IPage page in BrowserContextPageDictionary.Values)
//            {
//                await page.CloseAsync();
//            }
//            foreach (IBrowserContext browserContext in BrowserContexts.Values)
//            {
//                if (enableTracing)
//                {
//         
//                    await browserContext.Tracing.StopAsync(new()
//                    {
//                        Path = "trace.zip",
//                    });
//                }
//                await browserContext.CloseAsync();

//            }
//            Playwright.Dispose();
//            //await base.DisposeAsync();
//        }
//    }

}
}
