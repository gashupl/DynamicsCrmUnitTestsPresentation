using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Services
{
    public interface ICrmUnitOfWorkService
    {
        IAccountService AccountService { get; }
    }
}
