using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCRM
{
    /// <summary>
    /// Plugin stages
    /// </summary>
    public enum PluginStage
    {
        PreValidation = 10,
        PreOperation = 20,
        PostOperation = 40
    }
}
