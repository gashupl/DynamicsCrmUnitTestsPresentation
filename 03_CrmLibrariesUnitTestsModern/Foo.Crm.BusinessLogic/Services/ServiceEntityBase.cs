using Foo.Crm.BusinessLogic.Repositories;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Services
{
    public class ServiceEntityBase<E> : ServiceBase, IServiceEntityBase<E>  where E : Entity, new()
    {
        private IRepositoryEntity<E> _repositoryEntity;

        protected IRepositoryEntity<E> RepositoryEntity
        {
            get
            {
                return _repositoryEntity ?? this.UnitOfWorkRepository.GetRepositoryEntity<E>();
            }
        }

        public ServiceEntityBase() { }

        public IQueryable<E> GetAll()
        {
            var query = RepositoryEntity.GetAll();
            return query;
        }

        public IQueryable<E> GetAllForJoin(Func<E, bool> predicate)
        {
            var query = RepositoryEntity.GetAllForJoin(predicate);
            return query;
        }

        public IQueryable<E> GetByAttribute(string attributeName, object value, Func<E, E> selector)
        {
            return this.GetByAttribute(attributeName, value).Select(selector).AsQueryable();
        }

        public IQueryable<E> GetByAttribute(string attributeName, object value)
        {
            return this.RepositoryEntity.GetByAttribute(attributeName, value);
        }

        public E GetById(Guid id, Func<E, E> selector)
        {
            var query = RepositoryEntity.GetById(id);
            return query;
        }

        public E GetById(Guid id)
        {
            var query = RepositoryEntity.GetById(id);
            return query;
        }

        public E GetById(EntityReference enitityReference, Func<E, E> selector)
        {
            var query = RepositoryEntity.GetById(enitityReference.Id);
            return query;
        }

        public E GetById(EntityReference enitityReference)
        {
            var query = RepositoryEntity.GetById(enitityReference.Id);
            return query;
        }

        public Guid Create(E entity)
        {
            return this.RepositoryEntity.Create(entity);
        }

        public void Update(E entity)
        {
            this.RepositoryEntity.Update(entity);
        }

        public void Delete(Guid id)
        {
            this.RepositoryEntity.Delete(id);
        }

        public void UpdateMany(IEnumerable<E> entity)
        {
            this.RepositoryEntity.UpdateMany(entity);
        }


        public Guid AddItemToVirtualSource(E entity)
        {
            return this.RepositoryEntity.AddItemToVirtualSource(entity);
        }

        public void ClearVirtualSource()
        {
            this.RepositoryEntity.ClearVirtualSource();
        }

    }
}
