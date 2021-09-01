using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Sena.Mvc.Framework.Views.TagHelpers
{
    /// <summary>
    /// The modal-footer portion of Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement("modal-footer", ParentTag = "modal")]
    public class ModalFooterTagHelper : TagHelper
    {
        /// <summary>
        /// Whether or not to show a button to dismiss the dialog.
        /// Default: <c>true</c>
        /// </summary>
        public bool ShowDismiss { get; set; } = true;

        /// <summary>
        /// Whether or not the childrem elements will be added before (prepend) or after (append) the close button.
        /// Default: <c>true</c>
        /// </summary>
        public bool PrependChildren { get; set; } = true;

        /// <summary>
        /// The text to show on the Dismiss button
        /// Default: Cancel
        /// </summary>
        public string DismissText { get; set; } = "Fechar";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var dismissButton = @"<button type='button' class='btn btn-default' data-dismiss='modal' "
                               + "tabindex='99' data-original-title='Cancelar e fechar.' data-placement='auto'"
                               + ">{0}</button>";

            var childContent = await output.GetChildContentAsync();
            var footerContent = new DefaultTagHelperContent();

            if (ShowDismiss)
            {
                if (PrependChildren == true)
                {
                    footerContent.AppendHtml(childContent);
                    footerContent.AppendFormat(dismissButton, DismissText);
                }
                else
                {
                    footerContent.AppendFormat(dismissButton, DismissText);
                    footerContent.AppendHtml(childContent);
                }
            }
            else
            {
                footerContent.AppendHtml(childContent);
            }

            var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
            modalContext.Footer = footerContent;

            TagHelperAttribute idAttribute;
            if (context.AllAttributes.TryGetAttribute("id", out idAttribute))
            {
                modalContext.FooterId = idAttribute.Value.ToString();
            }

            output.SuppressOutput();
        }
    }
}