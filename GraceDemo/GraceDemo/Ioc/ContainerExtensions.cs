using Grace.DependencyInjection;
using Prism.Ioc;

namespace GraceDemo.Ioc
{
    public static class ContainerExtensions
    {
        public static IInjectionScope GetContainer(this IContainerRegistry containerRegistry) =>
            ((IContainerExtension<IInjectionScope>)containerRegistry).Instance;

        public static IInjectionScope GetContainer(this IContainerProvider containerProvider) =>
            ((IContainerExtension<IInjectionScope>)containerProvider).Instance;
    }
}
