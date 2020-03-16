using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vigente.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpaces(this string value)
        {
            return value.Trim();
        }

        /// <summary>
        /// Converte uma string para uma array de bytes UTF8.
        /// </summary>
        /// <param name="value">String de caracteres.</param>
        /// <returns>
        /// O valor convertido.
        /// </returns>
        public static byte[] AsByteArray(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
        
        /// <summary>
        /// Converte uma string em uma array de bytes com base 64.
        /// </summary>
        /// <param name="value">String de caracteres.</param>
        /// <returns>
        /// O valor convertido.
        /// </returns>
        public static byte[] AsStringBase64(this string value)
        {
            byte[] result = null;

            if (value != null)
            {
                value = Regex.Replace(value, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                result = Convert.FromBase64String(value);
            }

            return result;
        }
        
        /// <summary>
        /// Torna uma string segura para ser utilizada em javascripts.
        /// </summary>
        /// <param name="value">Valor atual da propriedade.</param>
        /// <returns></returns>
        public static string JavascriptSafe(this string value)
        {
            return System.Web.HttpUtility.JavaScriptStringEncode(value);
        }
    }
}
