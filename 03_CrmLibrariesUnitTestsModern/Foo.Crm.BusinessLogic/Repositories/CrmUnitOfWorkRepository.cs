using Foo.Crm.BusinessLogic.Common.Factories;
using Foo.Crm.BusinessLogic.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public class CrmUnitOfWorkRepository : ICrmUnitOfWorkRepository
    {
        private Dictionary<string, Repository> repositoriesDict;
        private RepositoriesFactory repositoriesFactory;


        public CrmUnitOfWorkRepository(IPluginExecutionContext context, IOrganizationServiceFactory service)
        {
            repositoriesDict = new Dictionary<string, Repository>();
            repositoriesFactory = new RepositoriesFactory(service, context.UserId);
        }

        public CrmUnitOfWorkRepository(IOrganizationService service)
        {
            repositoriesDict = new Dictionary<string, Repository>();
            repositoriesFactory = new RepositoriesFactory(service);
        }

        public CrmUnitOfWorkRepository()
        {
            repositoriesDict = new Dictionary<string, Repository>();
        }


        public IRepositoryEntity<Account> AccountRepository
        {
            get { return GetRepositoryEntity<Account>(); }
        }

        public IRepositoryEntity<E> GetRepositoryEntity<E>() where E : Entity, new()
        {
            IRepositoryEntity<E> result = null;
            Repository outRepository = null;
            string logicalName = new E().LogicalName;
            if (repositoriesDict.TryGetValue(logicalName, out outRepository))
            {
                result = (IRepositoryEntity<E>)outRepository;
            }
            else
            {
                result = repositoriesFactory.CreateRepositoryEntity<E>();
                repositoriesDict.Add(logicalName, (Repository)result);
            }

            return result;
        }

        public IRepositoryEntity GetRepositoryEntity(string logicalName)
        {
            IRepositoryEntity result = null;
            Repository outRepository = null;
            if (repositoriesDict.TryGetValue(logicalName, out outRepository))
            {
                result = (IRepositoryEntity)outRepository;
            }
            else
            {
                result = repositoriesFactory.CreateRepositoryEntity(logicalName);
                repositoriesDict.Add(logicalName, (Repository)result);
            }

            return result;
        }

        private IRepositoryMessage GetRepositoryMessagge()
        {
            string key = "RepositoryMessagge";
            IRepositoryMessage result = null;
            Repository outRepository = null;

            if (repositoriesDict.TryGetValue(key, out outRepository))
            {
                result = (IRepositoryMessage)outRepository;
            }
            else
            {
                result = repositoriesFactory.CreateRepositoryMessage();
                repositoriesDict.Add(key, (Repository)result);
            }

            return result;
        }


    }
}
