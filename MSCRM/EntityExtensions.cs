using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{

    /// <summary>
    /// Extension methods for Entity class
    /// </summary>
    public static class EntityExtensions
    {
        public static void Merge(this Entity target, Entity source)
        {
            foreach (var attribute in source.Attributes)
            {
                if (!target.Attributes.ContainsKey(attribute.Key))
                {
                    target.Attributes.Add(attribute);
                }
            }
        }
    }
}
