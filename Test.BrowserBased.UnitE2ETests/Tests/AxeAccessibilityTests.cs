using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Test.BrowserBased.UnitE2ETests.Helpers;
using Deque.AxeCore;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using FluentAssertions;
using Test.BrowserBased.UnitE2ETests.BlazeWright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Globalization;

namespace Test.BrowserBased.UnitE2ETests.Tests
{
    public class AxeAccessibilityTests : BlazorPageTest<Program>
    {
        [Theory]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Desktop)]

        public async Task CountIncrementerMeetsAxeAccesibilityStandards(string browserType, bool jsEnabled, ViewportHelper.ViewportType viewport)
        {



            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            IBrowserContext browserContext = await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, BaseUrl);
            //Debug option
            await browserContext.Tracing.StartAsync(new()
            {

                Screenshots = true,
                Snapshots = true,
                Sources = true,
          
            });


            IPage page = await browserContext.NewPageAsync();

          
            await page.GotoOnceNetworkIsIdleAsync("counter");


            AxeResult axeResults = await Page.RunAxe();

            axeResults.Violations.Should().BeNullOrEmpty();

     

            string methodName = "CountIncrementerMeetsAxeAccesibilityStandards";
            string timestamp = DateTime.UtcNow.ToString("yy_MM_dd_HH_mm_ss", CultureInfo.InvariantCulture);
            string arguments = $"{browserType}_{$"jsEnabled_{jsEnabled.ToString()}"}_{viewport.ToString()}";
            string path = $"../../../playwright-report/{methodName}_{arguments}_{timestamp}.zip";
            await browserContext.Tracing.StopAsync(new()
            {
                Path = path,

            });
            // Clean up resources by closing the page and browser context
            await page.CloseAsync();
            await browserContext.CloseAsync();
        }

     
    }
}
