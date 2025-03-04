using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Test.BrowserBased.UnitE2ETests.Helpers.ViewportHelper;

namespace Test.BrowserBased.UnitE2ETests.Helpers
{
    public static class BrowserHelper
    {
        
        public static async Task<IBrowserContext> CreateBrowserContextAsync(IPlaywright playwright, string browserType, bool jsEnabled, ViewportType viewport, string baseUrl/*, bool enableTracing = false*/)
        {

            IBrowser browser;
            bool headless = true;
            switch (browserType.ToLower())
            {
                case "chromium":
                    browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
                    break;
                case "firefox":
                    browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
                    break;
                case "webkit":
                    browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
                    break;
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }


             BrowserNewContextOptions contextOptions =   new BrowserNewContextOptions
            {
            
                JavaScriptEnabled = jsEnabled,
                BaseURL = baseUrl,
                IgnoreHTTPSErrors = true,
                ViewportSize = ViewportHelper.Viewports[viewport]
            };
            IBrowserContext context = await browser.NewContextAsync(contextOptions);
            //if (enableTracing) {
            //    await context.Tracing.StartAsync(new()
            //    {

            //        Screenshots = true,
            //        Snapshots = true,
            //        Sources = true
            //    });
            //}

            return context;
            //IPage page = await context.NewPageAsync();
            //return page;

        }

    }
}
