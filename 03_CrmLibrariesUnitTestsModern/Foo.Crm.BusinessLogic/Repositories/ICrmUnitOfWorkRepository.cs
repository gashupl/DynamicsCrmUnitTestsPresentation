using Foo.Crm.BusinessLogic.Model;
using Microsoft.Xrm.Sdk;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public interface ICrmUnitOfWorkRepository
    {
        IRepositoryEntity<Account> AccountRepository { get; }

        IRepositoryEntity<E> GetRepositoryEntity<E>() where E : Entity, new();
        IRepositoryEntity GetRepositoryEntity(string logicalName);
    }
}