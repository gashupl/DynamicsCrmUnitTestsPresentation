using Foo.Crm.BusinessLogic.Repositories;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Services
{
    public interface IServiceEntityBase<E> : IService
        where E : Entity
    {

        void Initialize(ICrmUnitOfWorkRepository crmUnitOfWorkRepository);

        IQueryable<E> GetAll();
        IQueryable<E> GetAllForJoin(Func<E, bool> predicate);

        IQueryable<E> GetByAttribute(string attributeName, object value, Func<E, E> selector);

        IQueryable<E> GetByAttribute(string attributeName, object value);

        E GetById(Guid id, Func<E, E> selector);

        E GetById(Guid id);

        E GetById(EntityReference enitityReference, Func<E, E> selector);

        E GetById(EntityReference enitityReference);


        Guid Create(E entity);

        void Update(E entity);
        void UpdateMany(IEnumerable<E> entity);

        void Delete(Guid id);


        Guid AddItemToVirtualSource(E entity);

        void ClearVirtualSource();

    }
}
