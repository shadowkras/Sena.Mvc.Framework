using Microsoft.AspNetCore.Mvc;

namespace Sena.Mvc.Framework.Views.ViewComponents
{
    /// <summary>
    /// Componente de selects usando Vue.
    /// <para>Documentação: https://vue-select.org/api/props.html </para>
    /// </summary>
    [ViewComponent(Name = "VueSelect")]
    public class VueSelect : ViewComponent
    {
        public IViewComponentResult Invoke(string propriedadeRetorno, string propriedadeId, string propriedadeDescricao, string valorInicial, string fetchUrl)
        {
            var viewModel = new VueSelectViewModel(propriedadeRetorno, propriedadeId, propriedadeDescricao, valorInicial ?? string.Empty, fetchUrl, string.Empty);
            return View("VueSelect", viewModel);
        }
    }

    public class VueSelectViewModel
    {
        #region Propriedade

        public string Id { get; }
        public string NomePropriedadeRetorno { get; }
        public string NomePropriedadeIdentificador { get; }
        public string NomePropriedadeDescricao { get; }
        public string ValorInicial { get; }
        public string FetchUrl { get; }

        /// <summary>
        /// TODO: Componente não está exibindo o placeholder definido.
        /// </summary>
        public string Placeholder { get; }

        #endregion

        #region Construtor

        public VueSelectViewModel(string propriedadeRetorno, string propriedadeId, string propriedadeDescricao, string valorInicial, string fetchUrl, string placeholder = null)
        {
            Id = "VueSelect" + propriedadeRetorno;
            NomePropriedadeRetorno = propriedadeRetorno;
            NomePropriedadeIdentificador = propriedadeId;
            NomePropriedadeDescricao = propriedadeDescricao;
            ValorInicial = valorInicial;
            FetchUrl = fetchUrl;
            Placeholder = placeholder;
        }

        #endregion
    }
}
