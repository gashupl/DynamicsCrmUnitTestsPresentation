using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public interface IRepositoryEntity
    {
        Guid Create(Entity entity);

        IEnumerable<Guid> CreateMany(IEnumerable<Entity> entities);

        void Delete(Guid id);

        void DeleteMany(IEnumerable<Guid> ids);

        IQueryable<Entity> GetAll();

        IQueryable<Entity> GetByAttribute(string attributeName, object value);

        Entity GetById(Guid id);

        void Update(Entity entity);

        void UpdateMany(IEnumerable<Entity> entities);
    }

    public interface IRepositoryEntity<E>
        where E : Entity
    {
        Guid Create(E entity);

        IEnumerable<Guid> CreateMany(IEnumerable<E> entities);

        void Delete(Guid id);

        void DeleteMany(IEnumerable<Guid> ids);

        IQueryable<E> GetAll();
     
        IQueryable<E> GetAllForJoin(Func<E, bool> predicate);

        IQueryable<E> GetByAttribute(string attributeName, object value);

        E GetById(Guid id);
        void Update(E entity);

        void UpdateMany(IEnumerable<E> entities);

        Guid AddItemToVirtualSource(E entity);

        void ClearVirtualSource();
    }
}