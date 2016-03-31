using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Actions
{
    public interface ICrmAction
    {
        bool CanWork();
        void DoWork();
    }
}
