using Microsoft.AspNetCore.Mvc;
using System;
using Sena.Mvc.Framework.Core.Extensions;

namespace Sena.Mvc.Framework.Views.Controllers
{
    /// <summary>
    /// Controller class with commonly used properties and methods used by API controllers.
    /// </summary>
    public class BaseApiController : Controller
    {
        /// <summary>
        /// Returns of the name of the controller without the "Controller" suffix.
        /// </summary>
        protected string ControllerName => this.GetType().Name.Replace("Controller", string.Empty);

        #region Construtor

        protected BaseApiController()
        { }

        #endregion

        #region Retorno padrão de API

        /// <summary>
        /// Returns our standard api object as json.
        /// </summary>
        /// <param name="isSuccess">Says wether the request was successful.</param>
        /// <param name="message">Message to return by our API.</param>
        /// <param name="data">Data returned by our API.</param>
        /// <returns></returns>
        public IActionResult ReturnAsApiJson(bool isSuccess = false, string message = "", object data = null)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return new ViewModels.RetornoApiViewModel
                {
                    IsSucess = isSuccess,
                    Message = message,
                    Data = data,
                }.ReturnAsJson();
            }
            else
            {
                return new ViewModels.RetornoApiViewModel
                {
                    IsSucess = isSuccess,
                    Message = message,
                    Data = data,
                }.ReturnAsJson();
            }
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
    }
}
