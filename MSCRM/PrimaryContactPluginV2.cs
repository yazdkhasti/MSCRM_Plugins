using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{
    /// <summary>
    /// Plugin to ensures that only a single contact can be marked as the primary contact for an account.
    /// The plugin uses OrganizationServiceContext to query data.
    /// OrganizationServiceContext class allows using the early-bound entity attributes instead of 
    /// hard-coding attribute names in multiple places
    /// </summary>
    public class PrimaryContactPluginV2 : PluginBase
    {
        protected override void ExecuteInternal(IPluginExecutionContext context, IOrganizationServiceFactory orgServiceFactory, ITracingService tracingService)
        {


            if (context.PrimaryEntityName == Contact.EntityLogicalName && context.Stage == (int)PluginStage.PostOperation)
            {
                tracingService.Trace("Primary Contact Plugin, Entity: {0}, Stage: {1}", context.PrimaryEntityName, context.Stage);

                tracingService.Trace("Retrieving the contact using post image with alias 'PostImage'");

                var contact = context.GetPostImage<Contact>("PostImage");

                if (contact.IsPrimaryContact.GetValueOrDefault(false) &&
                    contact.CompanyName != null && contact.CompanyName.LogicalName == Account.EntityLogicalName)
                {

                    tracingService.Trace("'Is Primary Contact' field is true and 'Primary Account' field is not empty.");

                    var accountRef = contact.CompanyName;

                    var orgService = orgServiceFactory.CreateOrganizationService(null);

                    var orgServiceContext = new OrganizationServiceContext(orgService)
                    {
                        MergeOption = MergeOption.NoTracking
                    };

                    using (orgServiceContext)
                    {
                        tracingService.Trace("Querying data using OrganizationServiceContext");

                        var relatedContacts = orgServiceContext.CreateQuery<Contact>()
                       .Where(x => x.IsPrimaryContact == true &&
                               x.CompanyName.Id == accountRef.Id &&
                               x.ContactId != contact.Id)
                       .Select(x => new Contact { Id = x.Id, IsPrimaryContact = x.IsPrimaryContact })
                       .ToList();

                        tracingService.Trace("Setting 'Is Primary Contact' field on contacts, Number of records: {0}", relatedContacts.Count);

                        foreach (var relatedContact in relatedContacts)
                        {
                            relatedContact.IsPrimaryContact = false;
                            orgService.Update(relatedContact);
                        }
                    }
                }

            }
        }
    }
}
