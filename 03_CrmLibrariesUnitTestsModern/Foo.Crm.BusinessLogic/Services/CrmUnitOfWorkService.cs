using Foo.Crm.BusinessLogic.Common.Factories;
using Foo.Crm.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Services
{
    public class CrmUnitOfWorkService : ICrmUnitOfWorkService
    {
        private IAccountService accountService;

        private ICrmUnitOfWorkRepository crmUnitOfWorkRepository;
        private ServicesFactory services;


        public CrmUnitOfWorkService(ICrmUnitOfWorkRepository crmUnitOfWorkRepository)
        {
            Initialize(crmUnitOfWorkRepository);
        }

        public void Initialize(ICrmUnitOfWorkRepository crmUnitOfWorkRepository)
        {
            this.crmUnitOfWorkRepository = crmUnitOfWorkRepository;
            this.services = new ServicesFactory(this.crmUnitOfWorkRepository);
        }

        // For mocking
        protected CrmUnitOfWorkService()
        {

        }
        public IAccountService AccountService
        {
            get { return accountService ?? (accountService = services.CreateService<AccountService>()); }
        }

    }
}
