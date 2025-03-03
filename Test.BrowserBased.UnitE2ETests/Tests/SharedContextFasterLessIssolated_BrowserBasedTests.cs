using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.BrowserBased.UnitE2ETests.Helpers;

namespace Test.BrowserBased.UnitE2ETests.Tests
{
    public class SharedContextFasterLessIssolated_BrowserBasedTests : BlazorPageTest<Program>
    {
        [Theory]
        [InlineData(BrowserConfig.ChromiumDesktopJS)]
        [InlineData(BrowserConfig.ChromiumDesktopNoJS, false)]
        [InlineData(BrowserConfig.ChromiumMobileJS)]
        [InlineData(BrowserConfig.ChromiumMobileNoJS, false)]
        [InlineData(BrowserConfig.FirefoxDesktopJS)]
        [InlineData(BrowserConfig.FirefoxDesktopNoJS, false)]
        [InlineData(BrowserConfig.FirefoxMobileJS)]
        [InlineData(BrowserConfig.FirefoxMobileNoJS, false)]
        [InlineData(BrowserConfig.WebkitDesktopJS)]
        [InlineData(BrowserConfig.WebkitDesktopNoJS, false)]
        [InlineData(BrowserConfig.WebkitMobileJS)]
        [InlineData(BrowserConfig.WebkitMobileNoJS,false)]

        public async Task Page_InteractivityIsCorrectlySimulated(BrowserConfig browserConfig, bool jsEnabled = true)
        {

            //qqqq what does this do maybe it should be the ipage i pass

            IPage page = await BrowserContexts[browserConfig].NewPageAsync();//equivalent same browser new tab
    
            //Debug option
            //await page.PauseAsync();

            //await page.GotoPreRenderedAsync("counter");qqqq try again now works
            await page.GotoAsync("counter", new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });
            ILocator status = page.GetByRole(AriaRole.Status);
            await Expect(status).ToHaveTextAsync("Current count: 0");

            // Find the button and click it
            ILocator button = page.GetByRole(AriaRole.Button, new() { Name = "Click me" }); // Change the name if needed
            await button.ClickAsync();

            // Check if JavaScript is enabled by verifying the text changes
            await Expect(status).ToHaveTextAsync(jsEnabled ? "Current count: 1" : "Current count: 0");

        

        }
    }
}
