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
        //If tracing is enabled it needs handling with dispose qqqq
        public static async Task<IBrowserContext> CreateBrowserContextAsync(IPlaywright playwright, string browserType, bool jsEnabled, ViewportType viewport, string baseUrl/*, bool enableTracing = false*/)
        {
            //qqqqq try catch
            //qqqqq it will be this using so we need to move this bit out
            //using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();
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
            //qqqq we probably want to set context options while testing so lets return the context
            return context;
            //IPage page = await context.NewPageAsync();
            //return page;

        }

    }
}
