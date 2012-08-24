using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleInjector;


namespace Shell.MVC2.DependencyResolution.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void Load<TModuleType>(this Container container)
            where TModuleType : class, ISimpleInjectorModule, new()
        {
            var x = new TModuleType();
            x.Load(container);
        }
    }
}
