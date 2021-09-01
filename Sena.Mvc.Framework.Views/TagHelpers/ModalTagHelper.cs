using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Sena.Mvc.Framework.Views.TagHelpers
{
    public class ModalContext
    {
        public string HeaderId { get; set; }
        public string BodyId { get; set; }
        public string FooterId { get; set; }

        public IHtmlContent Header { get; set; }
        public IHtmlContent Body { get; set; }
        public IHtmlContent Footer { get; set; }
    }

    /// <summary>
    /// A Bootstrap modal dialog
    /// </summary>
    [RestrictChildren("modal-header", "modal-body", "modal-footer")]
    public class ModalTagHelper : TagHelper
    {
        /// <summary>
        /// The title of the modal
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Id of the modal
        /// </summary>
        public string Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var modalContext = new ModalContext();
            context.Items.Add(typeof(ModalTagHelper), modalContext);

            await output.GetChildContentAsync();

            output.TagName = "div";
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("id", Id ?? context.UniqueId);
            output.Attributes.SetAttribute("aria-labelledby", $"{context.UniqueId}Label");
            output.Attributes.SetAttribute("tabindex", "-1");

            var classNames = " modal fade ";

            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Concat(output.Attributes["class"].Value, classNames);
            }

            var modalDialog = " modal-dialog ";

            if (output.Attributes["class"].Value.ToString().Contains("modal-large") == true)
            {
                modalDialog = string.Concat(modalDialog, " modal-lg ");
            }

            output.Attributes.SetAttribute("class", classNames);

            output.Content.AppendHtml(@"<div class='" + modalDialog + "' role='document'>" +
                                       "<div class='modal-content'>");

            if (modalContext.Header != null)
            {
                if (string.IsNullOrEmpty(modalContext.HeaderId) == true)
                    output.Content.AppendHtml("<div class='modal-header'>");
                else
                    output.Content.AppendHtml($"<div class='modal-header' id='{modalContext.HeaderId}'>");

                output.Content.AppendHtml(modalContext.Header);
                output.Content.AppendHtml("</div>");
            }
            else
            {
                output.Content.AppendHtml("<div class='modal-header'>");
                output.Content.AppendFormat(@"<h4 class='modal-title'>{0}</h4>", Title);
                output.Content.AppendFormat(@"<button type='button' class='close' data-dismiss='modal' data-original-title='Cancelar e fechar.' data-placement='left'><span aria-hidden='true'>x</button>");                
                output.Content.AppendHtml("</div>");
            }

            if (modalContext.Body != null)
            {
                if (string.IsNullOrEmpty(modalContext.BodyId) == true)
                    output.Content.AppendHtml("<div class='modal-body'>");
                else
                    output.Content.AppendHtml($"<div class='modal-body' id='{modalContext.BodyId}'>");

                output.Content.AppendHtml(modalContext.Body);
                output.Content.AppendHtml("</div>");
            }
            else
            {
                output.Content.AppendHtml("<div class='modal-body'>");
                output.Content.AppendHtml("</div>");
            }

            if (modalContext.Footer != null)
            {
                if (string.IsNullOrEmpty(modalContext.FooterId) == true)
                    output.Content.AppendHtml("<div class='modal-footer'>");
                else
                    output.Content.AppendHtml($"<div class='modal-footer' id='{modalContext.FooterId}'>");

                output.Content.AppendHtml(modalContext.Footer);
                output.Content.AppendHtml("</div>");
            }
            else
            {
                output.Content.AppendHtml("<div class='modal-footer'>");
                output.Content.AppendHtml(@"<button type = 'button' class='btn btn-default' data-dismiss='modal' "
                                         + "tabindex='99' data-original-title='Cancelar e fechar.' data-placement='auto'"
                                         + ">Fechar</button>");
                output.Content.AppendHtml("</div>");
            }

            output.Content.AppendHtml(@"</div></div>");
        }
    }
}