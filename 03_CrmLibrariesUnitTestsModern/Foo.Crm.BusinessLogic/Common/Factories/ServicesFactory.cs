using System;
using Microsoft.Xrm.Sdk;
using Foo.Crm.BusinessLogic.Repositories;
using Foo.Crm.BusinessLogic.Services;

namespace Foo.Crm.BusinessLogic.Common.Factories
{
    public class ServicesFactory
    {
       private ICrmUnitOfWorkRepository crmUnitOfWorkRepository { get; set; }

        public ServicesFactory(ICrmUnitOfWorkRepository crmUnitOfWorkRepository)
        {
            this.crmUnitOfWorkRepository = crmUnitOfWorkRepository;
        }

        public T CreateService<T>() where T : ServiceBase, new()
        {
            T service = new T();
            service.Initialize(this.crmUnitOfWorkRepository);
            return service;
        }
       
    }
}
