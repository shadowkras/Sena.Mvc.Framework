using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace Sena.Mvc.Framework.Views.Extensions
{
    /// <summary>
    /// Exntesion methods for UrlHelpers.
    /// </summary>
    public static class UrlHelperExtensions
    {
        #region Absolute Action

        /// <summary>
        /// Returns a string with the route path of a specific controller and action.
        /// </summary>
        /// <param name="url">UrlHelper class.</param>
        /// <param name="controllerName">Controller name that will be called.</param>
        /// <param name="actionName">Action name that will be called.</param>
        /// <param name="routeValues">Route values that will be called.</param>
        /// <returns>A URL absoluta.</returns>
        public static string AbsoluteAction(this UrlHelper url, string controllerName, string actionName, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        #endregion

        #region Absolute Content

        /// <summary>
        /// Returns a string URL with the path to the specified content.
        /// <para>Converts a virtual path (relative) to our application absolute path.</para>
        /// </summary>
        /// <param name="url">UrlHelper class.</param>
        /// <param name="contentPath">The content path.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteContent(this IUrlHelper url, string contentPath)
        {
            HttpRequest request = url.ActionContext.HttpContext.Request;
            return new Uri(new Uri(request.Scheme + "://" + request.Host.Value), url.Content(contentPath)).ToString();
        }

        #endregion

        #region Absolute Route Url

        /// <summary>
        /// Creates a "fully qualified" URL route using the specified route name and values.
        /// </summary>
        /// <param name="url">UrlHelper class interface.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">Route values that will be called.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteRouteUrl(this IUrlHelper url, string routeName, object routeValues = null)
        {
            return url.RouteUrl(routeName, routeValues, url.ActionContext.HttpContext.Request.Scheme);
        }

        #endregion

        #region Absolute Url

        /// <summary>
        /// Creates a "fully qualified" absolute url to an action of a controller on current area.
        /// </summary>
        /// <param name="url">Classe de UrlHelper.</param>
        /// <param name="controllerName">Controller name that will be called.</param>
        /// <param name="actionName">Action name that will be called.</param>
        /// <param name="routeValues">Route values that will be called.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this UrlHelper url, string controllerName, string actionName, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            var uri = url.Action(actionName, controllerName, routeValues, scheme);

            return uri;
        }

        /// <summary>
        /// Creates a "fully qualified" absolute url to an action on a specific controller and area.
        /// </summary>
        /// <param name="url">UrlHelper class.</param>
        /// <param name="areaName">Área onde a controller se localiza.</param>
        /// <param name="controllerName">Nome da controler com a action</param>
        /// <param name="actionName">Nome da action que será invocada.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this UrlHelper url, string areaName,
                                            string controllerName, string actionName)
        {
            var uri = GetAbsoluteUrl(url, controllerName, actionName, new { area = areaName });
            return uri;
        }

        /// <summary>
        /// Creates a "fully qualified" absolute url to an action on current area and controller.
        /// </summary>
        /// <param name="url">UrlHelper class interface.</param>
        /// <param name="actionName">Action name that will be called.</param>
        /// <param name="routeValues">Route values that will be called.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this IUrlHelper url, string actionName, object routeValues = null)
        {
            var urlHelper = new UrlHelper(url.ActionContext);
            var values = urlHelper.ActionContext.RouteData.Values;
            var controller = values["controller"].ToString();

            return GetAbsoluteUrl(url, controller, actionName, routeValues);
        }

        /// <summary>
        /// Creates a "fully qualified" absolute url to a controller and action on current area
        /// </summary>
        /// <param name="url">UrlHelper class interface.</param>
        /// <param name="controllerName">Controller name that will be called.</param>
        /// <param name="actionName">Action name that will be called.</param>
        /// <param name="routeValues">Route values that will be called.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this IUrlHelper url, string controllerName, string actionName, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        /// <summary>
        /// Creates a "fully qualified" absolute url to a controller and action.
        /// </summary>
        /// <param name="url">UrlHelper class interface.</param>
        /// <param name="areaName">Área onde a controller se localiza.</param>
        /// <param name="controllerName">Controller name that will be called.</param>
        /// <param name="actionName">Action name that will be called.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this IUrlHelper url, string areaName,
                                            string controllerName, string actionName)
        {
            var uri = GetAbsoluteUrl(url, controllerName, actionName, new { area = areaName });
            return uri;
        }

        #endregion
    }
}
