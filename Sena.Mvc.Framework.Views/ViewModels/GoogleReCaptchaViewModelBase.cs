using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Sena.Mvc.Framework.Views.Attributes;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    /// <summary>
    /// Classe base para validação de reCAPTCHA.
    /// </summary>
    public abstract class GoogleReCaptchaViewModelBase
    {
        [Required(ErrorMessage = "reCAPTCHA inválido ou não informado.")]
        [GoogleReCaptchaValidation]
        [JsonProperty("g-recaptcha-response")]
        public string GoogleReCaptchaResponse { get; set; }
    }
}
