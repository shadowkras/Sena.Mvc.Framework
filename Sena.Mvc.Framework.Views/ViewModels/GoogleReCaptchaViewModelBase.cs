using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Sena.Mvc.Framework.Views.Attributes;

namespace Sena.Mvc.Framework.Views.ViewModels
{
    /// <summary>
    /// Base class for google reCAPTCHA return.
    /// </summary>
    public abstract class GoogleReCaptchaViewModelBase
    {
        [Required(ErrorMessage = "reCAPTCHA invalid or not informed.")]
        [GoogleReCaptchaValidation]
        [JsonProperty("g-recaptcha-response")]
        public string GoogleReCaptchaResponse { get; set; }
    }
}
