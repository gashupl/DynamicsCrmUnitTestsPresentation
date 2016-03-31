using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Foo.Crm.BusinessLogic.Model;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public class RepositoryEntity<E> : Repository, IRepositoryEntity<E>, IRepositoryEntity
        where E : Entity, new()
    {
        protected IOrganizationService Service { get; set; }
        protected XrmServiceContext XrmContext { get; set; }
        protected List<E> VirtualSource { get; set; }

        protected string LogicalName
        {
            get
            {
                return (new E()).LogicalName;
            }
        } 

        public RepositoryEntity() { }

        public void Initialize(IOrganizationService service)
        {
            this.Service = service;
            this.XrmContext = new XrmServiceContext(service);
            this.XrmContext.MergeOption = Microsoft.Xrm.Sdk.Client.MergeOption.NoTracking;
            this.VirtualSource = new List<E>();
        }

        //Methods
        public IQueryable<E> GetAll()
        {
            var query = this.XrmContext.CreateQuery<E>();
            //IQueryable<E> unionQuery = VirtualSource.AsQueryable().Union(query);
            return query;
        }

        public E GetById(Guid id)
        {
            var query = this.GetAll().Single(q => q.Id == id);
            return query;
        }
       
        public Guid Create(E entity)
        {
            return this.Service.Create(entity);
        }

        public IEnumerable<Guid> CreateMany(IEnumerable<E> entities)
        {
            return entities.Select(item => this.Service.Create(item)).ToList();
        }

        public void Update(E entity)
        {
            this.Service.Update(entity);
        }

        public void UpdateMany(IEnumerable<E> entities)
        {
            foreach (E item in entities)
            {
                this.Service.Update(item);
            }

            //TODO: od update1
            //ExecuteTransactionRequest req = new ExecuteTransactionRequest()
            //{
            //    ReturnResponses = false,
            //    Requests = new OrganizationRequestCollection()
            //};

            //req.Requests.AddRange(entities.Select(e => new UpdateRequest() { Target = e }));

            //this.Service.Execute(req);
        }

        public void Delete(Guid id)
        {         
            this.Service.Delete(this.LogicalName, id);
        }

        public void DeleteMany(IEnumerable<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                this.Service.Delete(this.LogicalName, id);
            }
        }

        public IQueryable<E> GetAllForJoin(Func<E, bool> predicate)
        {
            return this.GetAll();
        }

        public IQueryable<E> GetByAttribute(string attributeName, object value)
        {
            var query = GetAll().Where(i => i[attributeName] == value);
            return query;
        }

        // IRepositoryEntity //
        IQueryable<Entity> IRepositoryEntity.GetAll()
        {
            return this.GetAll();
        }

        Entity IRepositoryEntity.GetById(Guid id)
        {
            return this.GetById(id);
        }

        Guid IRepositoryEntity.Create(Entity entity)
        {
            return this.Service.Create(entity);
        }

        IEnumerable<Guid> IRepositoryEntity.CreateMany(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                yield return this.Service.Create(entity);
            }
        }

        void IRepositoryEntity.Update(Entity entity)
        {
             this.Service.Update(entity);
        }

        void IRepositoryEntity.UpdateMany(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                this.Service.Update(entity);
            }
        }

        void IRepositoryEntity.Delete(Guid id)
        {
            this.Delete(id);
        }

        void IRepositoryEntity.DeleteMany(IEnumerable<Guid> ids)
        {
            this.DeleteMany(ids);
        }

        IQueryable<Entity> IRepositoryEntity.GetByAttribute(string attributeName, object value)
        {
            return this.GetAll().Where(i => i[attributeName] == value);
        }



        //Virtual Source
        public Guid AddItemToVirtualSource(E entity)
        {
            if (this.VirtualSource == null)
            {
                this.VirtualSource = new List<E>();
            }

            Guid newGuid = Guid.NewGuid();
            entity.Id = newGuid;
            this.VirtualSource.Add((E)entity);

            return newGuid; 
        }

        public void ClearVirtualSource()
        {
            this.VirtualSource.Clear();
        }


    }
}

