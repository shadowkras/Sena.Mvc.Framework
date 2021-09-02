using Microsoft.AspNetCore.Http;
using System;

namespace Sena.Mvc.Framework.Views.Extensions
{
    /// <summary>
    /// Extension methods for HttpRequest class.
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Convert a request Uri with schema, host, path and query string.
        /// </summary>
        /// <param name="request">HttpRequest object instance.</param>
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
