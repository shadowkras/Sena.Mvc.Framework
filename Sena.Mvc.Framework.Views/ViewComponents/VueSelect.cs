using Microsoft.AspNetCore.Mvc;

namespace Sena.Mvc.Framework.Views.ViewComponents
{
    /// <summary>
    /// Select component using VueJS.
    /// <para>Documentation: https://vue-select.org/api/props.html </para>
    /// </summary>
    [ViewComponent(Name = "VueSelect")]
    public class VueSelect : ViewComponent
    {
        public IViewComponentResult Invoke(string returnProperty, string idProperty, string descriptionProperty, string initialValue, string fetchUrl)
        {
            var viewModel = new VueSelectViewModel(returnProperty, idProperty, descriptionProperty, initialValue ?? string.Empty, fetchUrl, string.Empty);
            return View("VueSelect", viewModel);
        }
    }

    public class VueSelectViewModel
    {
        #region Propriedade

        public string Id { get; }
        public string ReturnPropertyName { get; }
        public string IdentifierPropertyName { get; }
        public string DescriptionPropertyName { get; }
        public string InitialValue { get; }
        public string FetchUrl { get; }

        /// <summary>
        /// TODO: Component is not exibiting the defined placeholder.
        /// </summary>
        public string Placeholder { get; }

        #endregion

        #region Construtor

        public VueSelectViewModel(string returnPropertyName, string identifierPropertyName, string descriptionPropertyName, string initialValue, string fetchUrl, string placeholder = null)
        {
            Id = "VueSelect" + returnPropertyName;
            ReturnPropertyName = returnPropertyName;
            IdentifierPropertyName = identifierPropertyName;
            DescriptionPropertyName = descriptionPropertyName;
            InitialValue = initialValue;
            FetchUrl = fetchUrl;
            Placeholder = placeholder;
        }

        #endregion
    }
}
