using System;
using StructureMap;

namespace Sena.Mvc.Framework.Services.DependencyInjection
{
    /// <summary>
    /// Structure map dependency resolver class.
    /// </summary>
    public class StructureMapDependencyResolver
    {
        public static Func<IContainer> ContainerAcesso { get; set; }
        private static IContainer Container => ContainerAcesso();

        public static T GetContainer<T>()
        {
            return Container.TryGetInstance<T>();
        }
    }
}
