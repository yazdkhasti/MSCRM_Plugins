using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{
    /// <summary>
    /// IPluginExecutionContext extension methods
    /// </summary>
    public static class PluginExecutionExtensions
    {
        public static T GetTargetEntity<T>(this IPluginExecutionContext context) where T : Entity
        {
            if (context.InputParameters.Contains("Target") &&
               context.InputParameters["Target"] is Entity)
            {
                var targetEntity = (Entity)context.InputParameters["Target"];
                return targetEntity.ToEntity<T>();
            }
            return null;
        }

        public static T GetPreImage<T>(this IPluginExecutionContext context, string name) where T : Entity
        {
            context.PreEntityImages.TryGetValue(name, out Entity image);
            return image?.ToEntity<T>();
        }

        public static T GetPostImage<T>(this IPluginExecutionContext context, string name) where T : Entity
        {
            context.PostEntityImages.TryGetValue(name, out Entity image);
            return image?.ToEntity<T>();
        }
    }
}
