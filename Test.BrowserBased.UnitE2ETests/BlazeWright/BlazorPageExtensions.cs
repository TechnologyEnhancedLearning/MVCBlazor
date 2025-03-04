using System.Diagnostics;

namespace Test.BrowserBased.UnitE2ETests.BlazeWright;

public static class BlazorPageExtensions
{
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static Task<IResponse?> GotoOnceNetworkIsIdleAsync(this IPage page, string url)//GotoPreRenderedAsync
        => page.GotoAsync(url, new() { WaitUntil = WaitUntilState.NetworkIdle });//this is by default waiting until prerendering is over
}
