using Microsoft.AspNetCore.Mvc;
using Sena.Mvc.Framework.Views.Extensions;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    /// <summary>
    /// Class to return standard API objects.
    /// </summary>
    public class ReturnApiViewModel
    {
        #region Propriedades Públicas

        /// <summary>
        /// Returns wether the request was successful.
        /// </summary>
        public bool IsSucess { get; set; }

        /// <summary>
        /// Message to return by our API. Usually used as display message for errors.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data returned by our API.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Defines if our APIs should return 404 if not successful.
        /// </summary>
        private bool? ReturnNotFound { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Standard constructor for an Return API class.
        /// </summary>
        /// <param name="isSuccess">Says wether the request was successful.</param>
        /// <param name="message">Message to return by our API.</param>
        /// <param name="data">Data returned by our API.</param>
        public ReturnApiViewModel(bool isSuccess = false, string message = "", object data = null, bool? returnNotFound = null)
        {
            IsSucess = isSuccess;
            Message = message;
            Data = data;
            ReturnNotFound = returnNotFound;
        }

        #endregion

        #region Métodos de Retorno

        /// <summary>
        /// Returns the Return API class as a json object.
        /// </summary>
        /// <param name="displayNulls">Defines if we should serialize null and empty objects.</param>
        /// <returns></returns>
        public ContentResult ReturnAsJson(bool displayNulls = false)
        {
            return new ContentResult
            {
                StatusCode = this.ReturnNotFound == true ? 404 : 200,
                Content = this.SerializeJSON(displayNulls),
                ContentType = "application/json"
            };
        }

        #endregion
    }
}
