using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;

namespace Sena.Mvc.Framework.Views.Attributes
{
    /// <summary>
    /// Attribute to validate google reCAPTCHA.
    /// </summary>
    public class GoogleReCaptchaValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Checks wether the reCAPTCHA is valid.
        /// </summary>
        /// <param name="value">Captcha response.</param>
        /// <param name="validationContext">Validation context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Lazy<ValidationResult> errorResult = new Lazy<ValidationResult>(() => new ValidationResult("Google reCAPTCHA invalid", new String[] { validationContext.MemberName }));

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return errorResult.Value;
            }

            IConfiguration configuration = (IConfiguration)validationContext.GetService(typeof(IConfiguration));
            string reCaptchResponse = value.ToString();
            string reCaptchaSecret = configuration.GetValue<string>("GoogleReCaptcha:SecretKey");

            HttpClient httpClient = new HttpClient();
            var httpResponse = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={reCaptchResponse}").Result;
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                return errorResult.Value;
            }

            string jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
            dynamic jsonData = JObject.Parse(jsonResponse);
            if (jsonData.success != true.ToString().ToLower())
            {
                return errorResult.Value;
            }

            return ValidationResult.Success;
        }
    }
}
