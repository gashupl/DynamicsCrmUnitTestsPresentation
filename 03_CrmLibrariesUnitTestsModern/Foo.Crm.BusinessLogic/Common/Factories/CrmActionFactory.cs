using Foo.Crm.BusinessLogic.Actions;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Repositories;
using Foo.Crm.BusinessLogic.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Common.Factories
{
    public class CrmActionFactory<E>  where E : new()
    {
        private Entity PreImage { get; set; }
        private Entity PostImage { get; set; }
        private Entity TargetEntity { get; set; }
        private HandlerCache Cache { get; set; }
        private IPluginExecutionContext Context { get; set; }
        private IOrganizationServiceFactory ServiceFactory { get; set; }

        private ITracingService TracingService { get; set; }

        private EntityReference TargetReference { get; set; }

        private ICrmUnitOfWorkService UnitOfWorkService { get; set; }

        public CrmActionFactory(IServiceProvider serviceProvider, HandlerCache handlerCache, ICrmUnitOfWorkService unitOfWorkService)
        {
            this.Initialize(serviceProvider);
            this.Cache = handlerCache;
            ICrmUnitOfWorkRepository crmUnitOfWorkRepository = new CrmUnitOfWorkRepository(this.Context, this.ServiceFactory);
            this.UnitOfWorkService = unitOfWorkService == null ? new CrmUnitOfWorkService(crmUnitOfWorkRepository) : unitOfWorkService;
        }

        protected void Initialize(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new NotSupportedException("IServiceProvider not defined");

            this.Context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            var proxyTypesAssembly = Context.GetType().GetProperty("ProxyTypesAssembly");

            if (proxyTypesAssembly != null)
            {
                proxyTypesAssembly.SetValue(Context, typeof(XrmServiceContext).Assembly, null);
            }

            this.TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            this.ServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            if (this.Context.InputParameters.Contains("Target"))
            {
                if (this.Context.InputParameters["Target"] is Entity)
                {
                    this.TargetEntity = ((Entity)this.Context.InputParameters["Target"]);
                    this.TargetReference = this.TargetEntity.ToEntityReference();
                }
                else if (this.Context.InputParameters["Target"] is EntityReference)
                {
                    this.TargetReference = this.Context.InputParameters["Target"] as EntityReference;
                    this.TargetEntity = null;
                    //this.TargetEntity.Id = this.TargetReference.Id;
                }
            }

            this.PreImage = this.Context.PreEntityImages.Contains("PreImage") ? ((Entity)this.Context.PreEntityImages["PreImage"]) : null;
            this.PostImage = this.Context.PostEntityImages.Contains("PostImage") ? ((Entity)this.Context.PostEntityImages["PostImage"]) : CreatePostImage();
        }

        private Entity CreatePostImage()
        {
            if (this.PreImage == null && this.TargetEntity == null)
                return null;

            Entity postImage = new Entity() { LogicalName = this.TargetEntity.LogicalName, Id = this.TargetEntity.Id };

            if (this.PreImage != null)
            {
                foreach (var attr in this.PreImage.Attributes)
                {
                    postImage.Attributes.Add(attr);
                }
            }

            foreach (var attr in this.TargetEntity.Attributes)
            {
                if (postImage.Attributes.Contains(attr.Key))
                    postImage[attr.Key] = attr.Value;
                else
                    postImage.Attributes.Add(attr);
            }

            return postImage;
        }

        public ICrmAction GetAction<T>()
            where T : CrmActionBase, new()
        {
            T action = new T();
            action.Initialize(this.Context, this.ServiceFactory, this.TracingService, this.Cache, this.TargetEntity, this.TargetReference, this.PreImage, this.PostImage, this.UnitOfWorkService);
            return action;
        }
    }
}

