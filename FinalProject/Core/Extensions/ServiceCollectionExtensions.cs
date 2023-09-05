using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyResolvers(this IServiceCollection serviceCollection, params ICoreModule [] modules) {

            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
        }
    }
}
