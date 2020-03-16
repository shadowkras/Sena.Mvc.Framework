using System;
using StructureMap;

namespace Vigente.Framework.Services.DependencyInjection
{
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
