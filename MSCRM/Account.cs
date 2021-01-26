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
    /// Account early-bound entity
    /// </summary>
    [EntityLogicalName(EntityLogicalName)]
    public class Account : Entity
    {
        public const string EntityLogicalName = "account";
        public Account() : base(EntityLogicalName)
        {
        }
    }
}
