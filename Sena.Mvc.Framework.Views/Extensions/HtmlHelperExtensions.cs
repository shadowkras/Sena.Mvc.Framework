using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Collections.Generic;
using Sena.Mvc.Framework.Core.Extensions;

namespace Sena.Mvc.Framework.Views.Extensions
{
    /// <summary>
    /// Extension methods for the HtmlHelper class.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region Obter o nome da action
        
        /// <summary>
        /// Return the action name that invoked our view.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper class interface.</param>
        /// <returns></returns>
        public static string ActionName(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
            {
                return (string)routeValues["action"];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Return the name of the current action on a view page.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper class interface</param>
        /// <returns></returns>
        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        #endregion

        #region Obter Url de Retorno

        /// <summary>
        /// Return the url that invoked the current request, to create a "return url".
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper class interface.</param>
        /// <returns></returns>
        public static string GetReturnUrl(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewContext?.HttpContext != null &&
                htmlHelper.ViewContext?.HttpContext.Request.Headers.IsEmpty() == false &&
                htmlHelper.ViewContext?.HttpContext.Request.Headers["Referer"].IsEmpty() == false)
            {
                return htmlHelper.ViewContext?.HttpContext.Request.Headers["Referer"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Obter SelectList de enumeradores

        /// <summary>
        /// Return a SelectList with the itens in an enum.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper class interface.</param>
        /// <param name="modelValue">Model value for default selected item.</param>
        /// <returns></returns>
        public static SelectList GetEnumSelectList<TEnum>(this IHtmlHelper htmlHelper, object modelValue) where TEnum : struct
        {
            System.Collections.IEnumerable items = ListEnum(typeof(TEnum));

            if (modelValue != null)
                return new SelectList(items, "Value", "Text", selectedValue: modelValue);
            else
                return new SelectList(items, "Value", "Text");
        }

        /// <summary>
        /// Return a list with the Display Names and values of the itens in an enumerator.
        /// </summary>
        /// <param name="source">Source enumerator.</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> ListEnum(this Enum source)
        {
            return ListEnum(source.GetType());
        }

        /// <summary>
        /// Return a list with the Display Names and values of the itens in an enumerator.
        /// </summary>
        /// <param name="source">Type deve ser do tipo Enum (enumerador).</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> ListEnum(Type source)
        {
            var type = source.GetType();
            return from Enum p in Enum.GetValues(source)
                   select new SelectListItem
                   {
                       Selected = p.Equals(type),
                       Text = p.EnumDescription(),
                       Value = Convert.ToInt32(p).ToString()
                   };
        }

        #endregion
    }
}
