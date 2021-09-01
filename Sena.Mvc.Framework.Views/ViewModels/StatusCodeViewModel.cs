using System.Collections.Generic;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    public class StatusCodeViewModel
    {
        public int StatusCode { get; private set; }
        public List<string> Mensagens { get; private set; }
        public string ReturnUrl { get; private set; }

        public StatusCodeViewModel(int statusCode, List<string> mensagens, string returnUrl)
        {
            StatusCode = statusCode;
            Mensagens = mensagens;
            ReturnUrl = returnUrl;
        }

        public StatusCodeViewModel(int statusCode, string mensagem, string returnUrl)
        {
            StatusCode = statusCode;
            Mensagens = new List<string>();
            Mensagens.Add(mensagem);
            ReturnUrl = returnUrl;
        }
    }
}
