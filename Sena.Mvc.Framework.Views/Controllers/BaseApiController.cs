using Microsoft.AspNetCore.Mvc;
using System;
using Sena.Mvc.Framework.Core.Extensions;

namespace Sena.Mvc.Framework.Views.Controllers
{
    public class BaseApiController : Controller
    {
        /// <summary>
        /// Retorna o nome da controller sem o texto "Controller".
        /// </summary>
        protected   string ControllerName => this.GetType().Name.Replace("Controller", string.Empty);

        #region Construtor

        protected BaseApiController()
        { }

        #endregion

        #region Retorno padrão de API

        /// <summary>
        /// Retorno em Json para métodos de API.
        /// </summary>
        /// <param name="sucesso">Operação executada com sucesso ou não.</param>
        /// <param name="mensagem">Mensagem de retorno para o usuário.</param>
        /// <param name="dados">Objeto de retorno para o usuário.</param>
        /// <returns></returns>
        public IActionResult RetornoApi(bool sucesso = false, string mensagem = "", object dados = null)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return new ViewModels.RetornoApiViewModel
                {
                    Sucesso = sucesso,
                    Mensagem = mensagem, //TODO pegar os erros da Model
                    Dados = dados,
                }.RetornoJson();
            }
            else
            {
                return new ViewModels.RetornoApiViewModel
                {
                    Sucesso = sucesso,
                    Mensagem = mensagem,
                    Dados = dados,
                }.RetornoJson();
            }
        }

        #endregion

        #region Retorna Mensagem de Erro (API)

        public IActionResult RetornaErrorApi(Exception ex, string mensagem = null)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return new ViewModels.RetornoApiViewModel
                {
                    Sucesso = false,
                    Mensagem = mensagem ?? "Ocorreu um erro.",
                    Dados = ex.GetMessageList(),
                }.RetornoJson();
            }
            else
            {
                return new ViewModels.RetornoApiViewModel
                {
                    Sucesso = false,
                    Mensagem = mensagem ?? "Ocorreu um erro.",
                }.RetornoJson();
            }
        }

        #endregion
    }
}
