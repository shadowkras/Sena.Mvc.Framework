using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Sena.Mvc.Framework.Views.TagHelpers
{
    /// <summary>
    /// The modal-footer portion of Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement("modal-header", ParentTag = "modal")]
    public class ModalHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// Whether or not to show a button to dismiss the dialog.
        /// Default: <c>true</c>
        /// </summary>
        public bool ShowDismiss { get; set; } = true;

        /// <summary>
        /// The text to show on the Dismiss button
        /// Default: x
        /// </summary>
        public string DismissText { get; set; } = "x";

        /// <summary>
        /// The text to show on the Title of the modal.
        /// Default: Título
        /// </summary>
        public string Title { get; set; } = "Título";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var headerContent = new DefaultTagHelperContent();

            if (childContent.IsEmptyOrWhiteSpace == false)
            {
                headerContent.AppendHtml(childContent);
            }
            else
            {
                headerContent.AppendFormat(@"<h4 class='modal-title'>{0}</h4>", Title);
            }

            if (ShowDismiss)
            {
                headerContent.AppendFormat(@"<button type='button' class='close' data-dismiss='modal' data-original-title='Cancelar e fechar.' data-placement='left'><span aria-hidden='true'>{0}</button>", DismissText);
            }

            var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
            modalContext.Header = headerContent;

            TagHelperAttribute idAttribute;
            if (context.AllAttributes.TryGetAttribute("id", out idAttribute))
            {
                modalContext.HeaderId = idAttribute.Value.ToString();
            }

            output.SuppressOutput();
        }
    }
}