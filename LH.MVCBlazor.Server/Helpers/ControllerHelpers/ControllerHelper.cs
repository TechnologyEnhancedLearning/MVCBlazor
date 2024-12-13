using Package.Shared.BlazorComponents.Enums;

namespace LH.MVCBlazor.Server.Helpers.ControllerHelpers
{
    public static class ControllerHelper
    {
        public static string GetRenderModeStr(string route, string controller)
        {
            // Get the current route from the request path
            

            // Extract the part of the route that matches the render mode
            string renderModeString = route.Replace($"/{controller}/", "").Replace("-MVCRendered", "");

            // Try to parse the extracted string into the enum
            if (Enum.TryParse(renderModeString, true, out GB_ComponentTagRenderMode renderMode))
            {
                // Successfully parsed render mode
            }
            else
            {
                // Default to Static if no match
                renderMode = GB_ComponentTagRenderMode.Static;
                //This could be the noJs so lets do static as a fallback

            }

            return renderMode.ToString();
        }
    }
}
