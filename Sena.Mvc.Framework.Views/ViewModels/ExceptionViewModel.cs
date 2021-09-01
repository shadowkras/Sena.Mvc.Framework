using System;
using System.Collections.Generic;
using System.Text;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    public class ExceptionViewModel
    {
        public string Titulo { get; private set; }
        public string Mensagem { get; private set; }
        public string Source { get; private set; }
        public string StackTrace { get; private set; }

        public ExceptionViewModel(Exception exception, string titulo, string mensagem)
        {
            Titulo = titulo;
            Mensagem = mensagem;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
        }
    }
}
