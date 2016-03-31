using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Common.Consts
{
    public enum PluginExecutionStageEnum
    {
        Validate = 10,
        PreOperation = 20,
        PostOperation = 40
    }
}
