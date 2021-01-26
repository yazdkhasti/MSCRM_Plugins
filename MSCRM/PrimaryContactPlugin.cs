using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{

    /// <summary>
    /// Plugin to ensures that only a single contact can be marked as the primary contact for an account.
    /// The implementation uses RetrieveMultiple and QueryExpression.
    /// </summary>
    public class PrimaryContactPlugin : PluginBase
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

                    var query = new QueryExpression
                    {
                        EntityName = Contact.EntityLogicalName,
                        ColumnSet = new ColumnSet("new_isprimarycontact"),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                                {
                                        new ConditionExpression
                                        {
                                            AttributeName = "parentcustomerid",
                                            Operator = ConditionOperator.Equal,
                                            Values = { accountRef.Id }
                                        },
                                        new ConditionExpression
                                        {
                                            AttributeName = "new_isprimarycontact",
                                            Operator = ConditionOperator.Equal,
                                            Values = { true }
                                        },
                                         new ConditionExpression
                                        {
                                            AttributeName = "contactid",
                                            Operator = ConditionOperator.NotEqual,
                                            Values = { contact.Id }
                                        }
                                 }
                        }
                    };

                    var orgService = orgServiceFactory.CreateOrganizationService(null);

                    tracingService.Trace("Querying data using QueryExpression");

                    var queryResult = orgService.RetrieveMultiple(query);

                    var relatedContacts = queryResult.Entities.Select(x => x.ToEntity<Contact>());

                    tracingService.Trace("Setting 'Is Primary Contact' field on contacts, Number of records: {0}", relatedContacts.Count());

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
