using Foo.Crm.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Services
{
    public class ServiceBase : IService
    {
        protected ICrmUnitOfWorkRepository UnitOfWorkRepository { get; private set; }

        public void Initialize(ICrmUnitOfWorkRepository unitOfWorkRepository)
        {
            this.UnitOfWorkRepository = unitOfWorkRepository;
        }
    }
}
