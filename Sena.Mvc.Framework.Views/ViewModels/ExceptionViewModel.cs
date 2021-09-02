using System;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    /// <summary>
    /// View model class for exception errors.
    /// </summary>
    public class ExceptionViewModel
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Source { get; private set; }
        public string StackTrace { get; private set; }

        public ExceptionViewModel(Exception exception, string title, string message)
        {
            Title = title;
            Message = message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
        }
    }
}
