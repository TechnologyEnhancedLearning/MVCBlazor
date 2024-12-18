using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.Core.Compliance.AccessibilityInterfaces
{
    public interface IGB_AccessibleComponent
    {
        /// Inevitably we will fall in the habit of putting the same accessibly text for everything so hopefully being able to hover the param and get custom suggestions will help qqqq
        /// <summary>
        /// The accessible label for the button for screen readers.
        /// </summary>
        /// <remarks>
        /// Provide a clear description of what the button does. 
        /// Example: "Save the current list to the database".
        /// This should be informative enough for users who rely on assistive technologies. 
        /// Use action-oriented language.
        /// </remarks>
        public string AriaLabel { get; set; }

        /// <summary>
        /// The title attribute for the button, shown as a tooltip.
        /// </summary>
        /// <remarks>
        /// Use a brief message that reinforces the button's action. 
        /// Example: "Click to save the current list".
        /// Tooltips can provide additional context or information to users hovering over the button.
        /// </remarks>
        public string ToolTipTitle { get; set; }

        /// <summary>
        /// Additional text for assistive technologies.
        /// </summary>
        /// <remarks>
        /// Use this property to convey any additional context or important notes about the button's action
        /// that might not be covered by the ButtonText or AriaLabel.
        /// Example: "This action will save all unsaved changes."
        /// </remarks>
        public string AssistiveText { get; set; }

        /// <summary>
        /// The ARIA role of the button.
        /// </summary>
        /// <remarks>
        /// Define the role of the button to provide additional context for assistive technologies.
        /// Example: "button".
        /// Use roles that accurately describe the element's function. If unsure, refer to 
        /// the ARIA roles documentation to choose the appropriate role.
        /// </remarks>
        public string AriaRole { get; set; }

        // Aria-describedby attribute for additional descriptive text
        public string AriaDescribedBy { get; set; }

        /// <summary>
        /// The tabindex of the button.
        /// </summary>
        /// <remarks>
        /// Use a positive integer to define the tab order of the button. 
        /// Example: 0.
        /// The default is 0, which includes it in the natural tab order. 
        /// This property helps with keyboard navigation.
        /// </remarks>
        public int TabIndex { get; set; }
    }
}
