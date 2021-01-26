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
    /// OrganisationService extension methods
    /// </summary>
    public static class OrganisationServiceExtensions
    {
        public static T Retrieve<T>(this IOrganizationService orgService, string entityName, Guid entityId) where T : Entity
        {
            var entity = orgService.Retrieve(entityName, entityId, new ColumnSet(true));
            return entity != null ? entity.ToEntity<T>() : null;
        }
    }
}
