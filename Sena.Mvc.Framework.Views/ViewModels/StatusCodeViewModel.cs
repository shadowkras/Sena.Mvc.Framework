using System.Collections.Generic;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    /// <summary>
    /// View model for status code views.
    /// </summary>
    public class StatusCodeViewModel
    {
        public int StatusCode { get; private set; }
        public List<string> Messages { get; private set; }
        public string ReturnUrl { get; private set; }

        public StatusCodeViewModel(int statusCode, List<string> messages, string returnUrl)
        {
            StatusCode = statusCode;
            Messages = messages;
            ReturnUrl = returnUrl;
        }

        public StatusCodeViewModel(int statusCode, string message, string returnUrl)
        {
            StatusCode = statusCode;
            Messages = new List<string>();
            Messages.Add(message);
            ReturnUrl = returnUrl;
        }
    }
}
