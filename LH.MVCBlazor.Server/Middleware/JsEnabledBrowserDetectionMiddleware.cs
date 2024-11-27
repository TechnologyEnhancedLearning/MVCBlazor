namespace LH.MVCBlazor.Server.Middleware
{
    namespace LH.MVCBlazor.Server.Middleware
    {

        //We want to check only once at the start of the session if Js is enabled and then on provide that to our blazor components so they can render accordingly first time

        public class JsEnabledBrowserDetectionMiddleware
        {
            private readonly RequestDelegate _next;

            public JsEnabledBrowserDetectionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                // Check if the 'jsEnabled' cookie exists and is set to 'true'
                var jsCookie = context.Request.Cookies["jsEnabled"];
                var JsEnabled = context.Session.GetString("JsEnabled");

                // If the cookie indicates JavaScript is enabled and session doesn't have 'JsEnabled' set
                if (jsCookie == "true" && string.IsNullOrEmpty(JsEnabled))
                {
                    context.Session.SetString("JsEnabled", "true");
                }

                // Continue processing the request
                await _next(context);
            }
        }
    }

}
