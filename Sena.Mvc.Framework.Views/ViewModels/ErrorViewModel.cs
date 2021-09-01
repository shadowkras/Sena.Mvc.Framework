using System.Collections.Generic;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    public class ErrorViewModel
    {
        public string Titulo { get; private set; }
        public List<string> Mensagens { get; private set; }
        public string ReturnUrl { get; private set; }

        public ErrorViewModel(string titulo, List<string> mensagens, string returnUrl)
        {
            Titulo = titulo;
            Mensagens = mensagens;
            ReturnUrl = returnUrl;
        }

        public ErrorViewModel(string titulo, string mensagem, string returnUrl)
        {
            Titulo = titulo;
            Mensagens = new List<string>();
            Mensagens.Add(mensagem);
            ReturnUrl = returnUrl;
        }
    }
}
