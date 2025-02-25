using Microsoft.Extensions.Options;
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

        public static async Task<IPage> CreatePageAsync(IPlaywright playwright, string browserType, bool jsEnabled, ViewportType viewport, string baseUrl)
        {
            //qqqqq try catch
            //qqqqq it will be this using so we need to move this bit out
            //using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            IBrowser browser;

            switch (browserType.ToLower())
            {
                case "chromium":
                    browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                    break;
                case "firefox":
                    browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                    break;
                case "webkit":
                    browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
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
            IPage page = await context.NewPageAsync();

            return page;

        }

    }
}
