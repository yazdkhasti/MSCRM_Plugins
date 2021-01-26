using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{
    /// <summary>
    /// IServiceProvider extension methods
    /// </summary>
    public static class ServiceProviderExtensions
    {
        private static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
        public static ITracingService GetTracingService(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<ITracingService>();
        }

        public static IPluginExecutionContext GetPluginExecutionContext(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IPluginExecutionContext>();
        }

        public static IOrganizationServiceFactory GetOrganizationServiceFactory(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IOrganizationServiceFactory>();
        }


    }
}
