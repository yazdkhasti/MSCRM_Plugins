using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace MSCRM
{

    /// <summary>
    /// Contact early-bound entity
    /// </summary>
    [EntityLogicalName(EntityLogicalName)]
    public class Contact : Entity
    {

        public const string EntityLogicalName = "contact";
        public Contact() : base(EntityLogicalName)
        {
        }


        // ContactId attribute should be used when quering using the OrgContext
        [AttributeLogicalName("contactid")]
        public Guid? ContactId
        {
            get { return this.GetAttributeValue<Guid?>("contactid"); }
            set { SetAttributeValue("contactid", value); }
        }

        [AttributeLogicalName("mobilephone")]
        public string MobilePhone
        {
            get { return this.GetAttributeValue<string>("mobilephone"); }
            set { SetAttributeValue("mobilephone", value); }
        }

        [AttributeLogicalName("mobilephone")]
        public string Email
        {
            get { return this.GetAttributeValue<string>("emailaddress1"); }
            set { SetAttributeValue("emailaddress1", value); }
        }

        [AttributeLogicalName("telephone1")]
        public string Telephone
        {
            get { return this.GetAttributeValue<string>("telephone1"); }
            set { SetAttributeValue("telephone1", value); }
        }

        [AttributeLogicalName("new_isprimarycontact")]
        public bool? IsPrimaryContact
        {
            get { return this.GetAttributeValue<bool?>("new_isprimarycontact"); }
            set { SetAttributeValue("new_isprimarycontact", value); }
        }

        [AttributeLogicalName("parentcustomerid")]
        public EntityReference CompanyName
        {
            get { return this.GetAttributeValue<EntityReference>("parentcustomerid"); }
            set { SetAttributeValue("parentcustomerid", value); }
        }
    }
}
