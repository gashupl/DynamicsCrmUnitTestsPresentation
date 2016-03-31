using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Repositories;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Common.Factories
{
    public class RepositoriesFactory
    {
        /// <summary>
        /// Domyślny konstrutor stosowany w pluginach i przepływach pracy
        /// </summary>
        /// <param name="serviceFactory"></param>
        public RepositoriesFactory(IOrganizationServiceFactory serviceFactory, Guid? userId)
        {
            this.ServiceFactory = serviceFactory;
            this.DefaultService = this.ServiceFactory.CreateOrganizationService(userId);
        }

        //private Dictionary<Type, Repositories.RepositoryBase> Repositories { get; set; }
        /// <summary>
        /// Konstruktor używany w zewnętrznych aplikacjach np. w jobach
        /// </summary>
        /// <param name="service"></param>
        public RepositoriesFactory(IOrganizationService service)
        {
            this.DefaultService = service;
        }

        public IOrganizationService DefaultService { get; private set; }
        private IOrganizationServiceFactory ServiceFactory { get; set; }
        public IRepositoryEntity<E> CreateRepositoryEntity<E>() where E : Entity, new()
        {           
                RepositoryEntity<E> repo = new RepositoryEntity<E>();
                repo.Initialize(this.DefaultService);
                return repo;
        }

        public IRepositoryEntity CreateRepositoryEntity(string entityLogicalName)
        {
            return GetRepositoryByLogicalName(entityLogicalName);
        }

        public IRepositoryMessage CreateRepositoryMessage()
        {
            RepositoryMessage repo = new RepositoryMessage();
            repo.Initialize(this.DefaultService);
            return repo;
        }

        private IRepositoryEntity GetRepositoryByLogicalName(string entityLogicalName)
        {
            switch (entityLogicalName)
            {
                case Account.EntityLogicalName:
                    return (IRepositoryEntity)CreateRepositoryEntity<Account>();                   
                default:
                    throw new NotImplementedException("Not Implemented Map Repository");
            }
        }

        //public RepositoryEntity<E> CreateElevatedPrivilegeRepositoryEntity<E>() where E : Entity, new()
        //{
        //    if (this.ServiceFactory == null)
        //        return this.CreateRepositoryEntity<E>();


        //    Guid systemUser = GetSystemUser();

        //    RepositoryEntity<E> repo = new RepositoryEntity<E>();
        //    repo.Initialize(this.ServiceFactory.CreateOrganizationService(systemUser));
        //    return repo;

        //}
        //public RepositoryMessage CreateElevatedPrivilegeRepositoryMessage()
        //{
        //    if (this.ServiceFactory == null)
        //        return this.CreateRepositoryMessage();


        //    Guid systemUser = GetSystemUser();

        //    RepositoryMessage repo = new RepositoryMessage();
        //    repo.Initialize(this.ServiceFactory.CreateOrganizationService(systemUser));
        //    return repo;

        //}

        private Guid GetSystemUser()
        {
            var service = this.ServiceFactory.CreateOrganizationService(null);

            // Get admin user Guid here :)
            return Guid.Empty;

        }
    }
}
