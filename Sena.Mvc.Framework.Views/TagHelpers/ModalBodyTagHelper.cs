using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Sena.Mvc.Framework.Views.TagHelpers
{
    /// <summary>
    /// The modal-body portion of a Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement("modal-body", ParentTag = "modal")]
    public class ModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
            modalContext.Body = childContent;

            TagHelperAttribute idAttribute;
            if (context.AllAttributes.TryGetAttribute("id", out idAttribute))
            {
                modalContext.BodyId = idAttribute.Value.ToString();
            }

            output.SuppressOutput();
        }
    }
}