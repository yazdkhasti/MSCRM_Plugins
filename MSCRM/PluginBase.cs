using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{

    /// <summary>
    /// Base class for developing plugins.
    /// The class provides the basic functionality for implementing the plug-ins
    /// </summary>
    public abstract class PluginBase : IPlugin
    {
        /// <summary>
        /// Execute method that is required by the IPlugin interface.
        /// </summary>
        /// <param name="serviceProvider">The service provider from which you can obtain the
        /// tracing service, plug-in execution context, organization service, and more.</param>
        public void Execute(IServiceProvider serviceProvider)
        {

            //Extract the tracing service for use in debugging sandboxed plug-ins.
            ITracingService tracingService = serviceProvider.GetTracingService();


            //Obtain the execution context from the service provider.
            IPluginExecutionContext context = serviceProvider.GetPluginExecutionContext();


            //Obtain the organisation service factory from the service provider.
            IOrganizationServiceFactory orgServiceFactory = serviceProvider.GetOrganizationServiceFactory();

            try
            {
                ExecuteInternal(context, orgServiceFactory, tracingService);
            }
            catch (InvalidPluginExecutionException ex)
            {
                tracingService.Trace("An InvalidPluginExecutionException was thrown: {0}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                tracingService.Trace("An unexpected exception was thrown: {0}", ex.ToString());
                throw new InvalidPluginExecutionException("An unexpected error occured.", ex);
            }



        }

        protected abstract void ExecuteInternal(IPluginExecutionContext context, IOrganizationServiceFactory orgServiceFactory, ITracingService tracingService);
    }
}
