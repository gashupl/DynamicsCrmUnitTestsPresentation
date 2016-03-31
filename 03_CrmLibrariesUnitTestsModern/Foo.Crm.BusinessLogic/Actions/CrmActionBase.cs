using Foo.Crm.BusinessLogic.Common;
using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Actions
{
    public abstract class CrmActionBase : ICrmAction
    {
        // CRM doklejka kropki do nazw relacji NN w 'Relationship'
        private const string MAGIC_CRM_DOT = ".";

        public CrmActionBase() { }

        protected HandlerCache Cache { get; set; }

        protected IPluginExecutionContext Context { get; set; }

        protected Entity PostImage { get; set; }

        protected Entity PreImage { get; set; }

        protected EntityReferenceCollection RelatedEntities { get; set; }

        // Associate
        protected string RelationshipName { get; private set; }

        protected Entity TargetEntity { get; set; }

        protected EntityReference TargetReference { get; set; }

        protected ITracingService TracingService { get; set; }

        //protected RepositoriesFactory Repositories { get; private set; }
        protected ICrmUnitOfWorkService CrmUnitOfWorkService { get; private set; }
        public abstract bool CanWork();

        public abstract void DoWork();

        public void Initialize(IPluginExecutionContext context, IOrganizationServiceFactory service, ITracingService tracingService, HandlerCache cache, Entity target, EntityReference targetReference, Entity preImage, Entity postImage, ICrmUnitOfWorkService unitOfWorkRepository)
        {
            this.Cache = cache;
            this.Context = context;
            this.CrmUnitOfWorkService = unitOfWorkRepository;
            this.TargetEntity = target;
            this.TargetReference = targetReference;
            this.PreImage = preImage;
            this.PostImage = postImage;
            this.TracingService = tracingService;

            this.InitializaRelatedEntities();
        }

        protected void ForceUpdatePreImageRegistration()
        {
            if (this.Context.MessageName == CrmMessagesNames.Update && this.PreImage == null)
                throw new InvalidPluginExecutionException("Pre Image is empty");
        }

        protected TEntity GetPostImage<TEntity>()
            where TEntity : Entity, new()
        {
            if (PostImage == null)
                return null;

            string tEntityType = (new TEntity()).LogicalName;

            if (PostImage.LogicalName != tEntityType)
                throw new ArgumentException(String.Format("Wrong entity type specified ({0}). Expected {1} entity type.", tEntityType, PostImage.LogicalName));

            return PostImage.ToEntity<TEntity>();
        }

        protected TEntity GetPreImage<TEntity>()
            where TEntity : Entity, new()
        {
            if (PreImage == null)
                return null;

            string tEntityType = (new TEntity()).LogicalName;

            if (PreImage.LogicalName != tEntityType)
                throw new ArgumentException(String.Format("Wrong entity type specified ({0}). Expected {1} entity type.", tEntityType, PreImage.LogicalName));

            return PreImage.ToEntity<TEntity>();
        }

        protected TEntity GetTargetEntity<TEntity>()
            where TEntity : Entity, new()
        {
            if (TargetEntity == null)
                return null;

            string tEntityType = (new TEntity()).LogicalName;

            if (TargetEntity.LogicalName != tEntityType)
                throw new ArgumentException(String.Format("Wrong entity type specified ({0}). Expected {1} entity type.", tEntityType, TargetEntity.LogicalName));

            return TargetEntity.ToEntity<TEntity>();
        }

        protected bool IsRelationshipValid(string relationshipName)
        {
            if (this.Context.MessageName != CrmMessagesNames.Associate) return false;

            if (this.Context != null && this.Context.InputParameters.Contains("Relationship"))
            {
                if ((relationshipName + MAGIC_CRM_DOT) != this.RelationshipName)
                    return false;
            }

            return true;
        }

        private void InitializaRelatedEntities()
        {
            if (this.Context != null &&
                this.Context.InputParameters.Contains("RelatedEntities"))
            {
                this.RelatedEntities = this.Context.InputParameters["RelatedEntities"] as EntityReferenceCollection;
                this.RelationshipName = this.Context.InputParameters["Relationship"].ToString();
            }
        }
    }
}
