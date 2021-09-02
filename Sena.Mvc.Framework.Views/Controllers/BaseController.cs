using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sena.Mvc.Framework.Core.Extensions;
using Sena.Mvc.Framework.Views.Extensions;

namespace Sena.Mvc.Framework.Views.Controllers
{
    /// <summary>
    /// Controller class with commonly used methods and properties.
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Returns wether the current request is a POST method.
        /// </summary>
        protected bool IsPOSTMethod => (HttpContext.Request.Method == "POST");

        /// <summary>
        /// Returns of the name of the controller without the "Controller" suffix.
        /// </summary>
        protected string ControllerName => this.GetType().Name.Replace("Controller", string.Empty);

        #region Construtor

        protected BaseController()
        { }

        #endregion

        #region Adicionar erro de Model

        /// <summary>
        /// Adds an error message to our mode state.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void AddModelError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        /// <summary>
        /// Adds an error message to our mode state.
        /// </summary>
        /// <param name="messages">List of error messages.</param>
        public void AddModelError(IList<string> messages)
        {
            foreach (var message in messages)
            {
                ModelState.AddModelError(string.Empty, message);
            }
        }
        #endregion

        #region Retorno de Json (API)

        /// <summary>
        /// Returns our standard api object as json.
        /// </summary>
        /// <param name="isSuccess">Says wether the request was successful.</param>
        /// <param name="message">Message to return by our API.</param>
        /// <param name="data">Data returned by our API.</param>
        /// <returns></returns>
        public IActionResult RetornAsApiJson(bool isSuccess = false, string message = "", object data = null)
        {
            return new ViewModels.RetornoApiViewModel
            {
                IsSucess = isSuccess,
                Message = message,
                Data = data,
            }.ReturnAsJson();
        }

        #endregion

        #region Retorna Mensagem de Erro (API)

        /// <summary>
        /// Return our standard api object with an error message in json format.
        /// </summary>
        /// <param name="ex">Exception with our error.</param>
        /// <param name="message">Friendly message.</param>
        /// <returns></returns>
        public IActionResult ReturnAsErrorApiJson(Exception ex, string message = null)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return new ViewModels.RetornoApiViewModel
                {
                    IsSucess = false,
                    Message = message ?? "There was an error.",
                    Data = ex.GetMessageList(),
                }.ReturnAsJson();
            }
            else
            {
                return new ViewModels.RetornoApiViewModel
                {
                    IsSucess = false,
                    Message = message ?? "There was an error.",
                }.ReturnAsJson();
            }
        }

        #endregion

        #region Obter o Request como string

        /// <summary>
        /// Returns the complete Uri of the current request.
        /// </summary>
        /// <returns></returns>
        public string GetRequestUri()
        {
            string requestUri = string.Empty;

            if (HttpContext != null && HttpContext.Request != null)
            {
                requestUri = HttpContext.Request.ToUri().AbsoluteUri;
            }

            return requestUri;
        }

        #endregion
    }
}
