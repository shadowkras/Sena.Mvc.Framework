using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace Vigente.Framework.Views.Extensions
{
    /// <summary>
    /// Classe de extensão para renderização de views do razor.
    /// </summary>
    public static class ViewEngineExtensions
    {
        /// <summary>
        /// Renderiza uma view com os valores da sua viewmodel e retorna como uma string.
        /// </summary>
        /// <param name="controller">Classe de Controller.</param>
        /// <param name="viewName">Nome da view.</param>
        /// <param name="model">Viewmodel com os valores para a view.</param>
        /// <param name="partial">Indica se a view é uma partial view ou não.</param>
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
                    return $"A view {viewName} não foi encontrada. Verifique o caminho e o nome do arquivo da view.";
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
        /// Renderiza uma view com os valores da sua viewmodel e retorna como uma string.
        /// </summary>
        /// <param name="razorPage">Classe de RazorPage.</param>
        /// <param name="viewName">Nome da view.</param>
        /// <param name="model">Viewmodel com os valores para a view.</param>
        /// <param name="partial">Indica se a view é uma partial view ou não.</param>
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
                    return $"A view {viewName} não foi encontrada. Verifique o caminho e o nome do arquivo da view.";
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
