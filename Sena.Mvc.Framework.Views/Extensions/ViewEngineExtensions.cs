using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace Sena.Mvc.Framework.Views.Extensions
{
    /// <summary>
    /// Extension class to render views using Razor.
    /// </summary>
    public static class ViewEngineExtensions
    {
        /// <summary>
        /// Renders a view with your view model values and returns as string.
        /// </summary>
        /// <param name="controller">Controller class.</param>
        /// <param name="viewName">Nome of your view.</param>
        /// <param name="model">Viewmodel with values for your view.</param>
        /// <param name="partial">Says wether its a partial view.</param>
        /// <returns></returns>
        public static async Task<string> RenderPartialViewToString(this Controller controller, string viewName, object model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            #region Criando ViewDictionary

            ViewDataDictionary newViewData = null;

            if (model == null)
            {
                if (controller.ViewData == null)
                {
                    newViewData = new ViewDataDictionary<object>(controller.ViewData, null);
                }
                else
                {
                    newViewData = new ViewDataDictionary(controller.ViewData);
                }
            }
            else
            {
                if (controller.ViewData == null)
                {
                    newViewData = new ViewDataDictionary(controller.ViewData);
                }
                else
                {
                    newViewData = new ViewDataDictionary<object>(controller.ViewData, model);
                }
            }

            #endregion

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.GetView(executingFilePath: viewName, viewPath: viewName, isMainPage: !partial);

                if (viewResult.Success == false)
                {
                    return $"The view {viewName} was not found. Check the path and the file name of the view.";
                }

                ViewContext viewContext = new ViewContext(controller.ControllerContext,
                                                          viewResult.View,
                                                          newViewData,
                                                          controller.TempData,
                                                          writer,
                                                          new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Renders a view with your view model values and returns as string.
        /// </summary>
        /// <param name="razorPage">RazorPage model class.</param>
        /// <param name="viewName">Nome of your view.</param>
        /// <param name="model">Viewmodel with values for your view.</param>
        /// <param name="partial">Says wether its a partial view.</param>
        /// <returns></returns>
        public static async Task<string> RenderPartialViewToString(this PageModel razorPage, string viewName, object model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = razorPage.PageContext.ActionDescriptor.Id;
            }

            #region Criando ViewDictionary

            ViewDataDictionary newViewData = null;

            if (model == null)
            {
                if (razorPage.PageContext.ViewData == null)
                {
                    newViewData = new ViewDataDictionary<object>(razorPage.PageContext.ViewData, null);
                }
                else
                {
                    newViewData = new ViewDataDictionary(razorPage.PageContext.ViewData);
                }
            }
            else
            {
                if (razorPage.PageContext.ViewData == null)
                {
                    newViewData = new ViewDataDictionary(razorPage.PageContext.ViewData);
                }
                else
                {
                    newViewData = new ViewDataDictionary<object>(razorPage.PageContext.ViewData, model);
                }
            }

            #endregion

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = razorPage.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.GetView(executingFilePath: viewName, viewPath: viewName, isMainPage: !partial);

                if (viewResult.Success == false)
                {
                    return $"The view {viewName} was not found. Check the path and the file name of the view.";
                }                

                //public ViewContext(ViewContext viewContext, IView view, ViewDataDictionary viewData, TextWriter writer);
                ViewContext viewContext = new ViewContext(razorPage.PageContext,
                                                          viewResult.View,
                                                          newViewData,
                                                          razorPage.TempData,
                                                          writer,
                                                          new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
