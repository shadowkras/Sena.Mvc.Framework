using Microsoft.AspNetCore.Http;
using System;

namespace Sena.Mvc.Framework.Views.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Converte o request em uma Uri com schema, host, path e query string.
        /// </summary>
        /// <param name="request">Objeto de HttpRequest.</param>
        /// <returns></returns>
        public static Uri ToUri(this HttpRequest request)
        {
            var hostComponents = request.Host.ToUriComponent().Split(':');

            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = hostComponents[0],
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            if (hostComponents.Length == 2)
            {
                builder.Port = Convert.ToInt32(hostComponents[1]);
            }

            return builder.Uri;
        }
    }
}
