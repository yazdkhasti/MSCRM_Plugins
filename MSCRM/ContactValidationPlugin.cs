using System;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MSCRM
{
    /// <summary>
    /// Plugin that checks if the contact has at least one contact method - either a mobile (mobilephone), home phone (telephone1), or email (emailaddress1).
    /// </summary>
    public sealed class ContactValidationPlugin : PluginBase
    {
        protected override void ExecuteInternal(IPluginExecutionContext context, IOrganizationServiceFactory orgServiceFactory, ITracingService tracingService)
        {

            if (context.PrimaryEntityName == Contact.EntityLogicalName && context.Stage == (int)PluginStage.PreValidation)
            {
                if (context.MessageName == MessageNames.Create || context.MessageName == MessageNames.Update)
                {

                    tracingService.Trace("Retriving the contact from the plugin context");

                    var contact = context.GetTargetEntity<Contact>();

                    if (context.MessageName == MessageNames.Update)
                    {
                       
                        tracingService.Trace("Retriving the contact pre-image");

                        var entityImage = context.GetPreImage<Contact>("PreImage");

                        if (entityImage == null)
                        {
                            tracingService.Trace("Retriving the contact from the organisation service because the pre-image is null.");

                            var orgService = orgServiceFactory.CreateOrganizationService(null);

                            entityImage = orgService.Retrieve<Contact>(Contact.EntityLogicalName, context.PrimaryEntityId);
                        }

                        contact.Merge(entityImage);

                    }

                    tracingService.Trace("Validation the contact:");
                    tracingService.Trace("Mobile Phone: {0}, Email: {1}, Telephone: {2}", contact.MobilePhone, contact.Email, contact.Telephone);

                    if (string.IsNullOrWhiteSpace(contact.MobilePhone)
                        && string.IsNullOrWhiteSpace(contact.Email)
                        && string.IsNullOrWhiteSpace(contact.Telephone))
                    {
                        throw new InvalidPluginExecutionException("At least one of the following contact methods must be provided: email, telephone or mobile");
                    }


                }

            }
        }
    }

}

