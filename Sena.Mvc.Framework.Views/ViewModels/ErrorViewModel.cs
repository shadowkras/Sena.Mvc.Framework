using System.Collections.Generic;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    /// <summary>
    /// View model class for error messages.
    /// </summary>
    public class ErrorViewModel
    {
        public string Title { get; private set; }
        public List<string> Messages { get; private set; }
        public string ReturnUrl { get; private set; }

        public ErrorViewModel(string title, List<string> messages, string returnUrl)
        {
            Title = title;
            Messages = messages;
            ReturnUrl = returnUrl;
        }

        public ErrorViewModel(string title, string message, string returnUrl)
        {
            Title = title;
            Messages = new List<string>();
            Messages.Add(message);
            ReturnUrl = returnUrl;
        }
    }
}
