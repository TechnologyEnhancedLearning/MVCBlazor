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

namespace Test.BrowserBased.UnitE2ETests.Tests
{
    public class AxeAccessibilityTests : BlazorPageTest<Program>
    {
        [Theory]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Desktop)]

        public async Task CountIncrementerMeetsAxeAccesibilityStandards(string browserType, bool jsEnabled, ViewportHelper.ViewportType viewport)
        {

            string baseUrl = Host.ServerAddress;


            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            IBrowserContext browserContext = await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, baseUrl);
            //Debug option
            await browserContext.Tracing.StartAsync(new()
            {

                Screenshots = true,
                Snapshots = true,
                Sources = true
            });


            IPage page = await browserContext.NewPageAsync();

          
            await page.GotoAsync("counter", new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });


            AxeResult axeResults = await Page.RunAxe();

            axeResults.Violations.Should().BeNullOrEmpty();

            await browserContext.Tracing.StopAsync(new()
            {
                Path = "trace.zip",
            });

        }

     
    }
}
