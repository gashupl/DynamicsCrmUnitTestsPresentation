using Foo.Crm.BusinessLogic.Actions;
using Foo.Crm.BusinessLogic.Common;
using Foo.Crm.BusinessLogic.Common.Factories;
using Foo.Crm.BusinessLogic.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Plugins
{
    public abstract class PluginBase<E> : IPlugin where E : new()
    {
        protected ICrmUnitOfWorkService CrmUnitOfWorkService { get; private set; }


        public PluginBase(ICrmUnitOfWorkService crmUnitOfWorkService)
        {
            this.CrmUnitOfWorkService = crmUnitOfWorkService;
        }

        public PluginBase()
        {

        }

        /// <summary>
        /// Register your actions by adding them from actionFactory to registeredActions.
        /// </summary>
        /// <example>registeredActions.Add(actionFactory.GetAction<PutYourActionHere>());</example>
        /// <param name="actionFactory"></param>
        /// <param name="registeredActions"></param>
        public abstract void RegisterActions(CrmActionFactory<E> actionFactory, List<ICrmAction> registeredActions);

        public void Execute(IServiceProvider serviceProvider)
        {
            HandlerCache cache = new HandlerCache();
            var actionFactory = new CrmActionFactory<E>(serviceProvider, cache, this.CrmUnitOfWorkService);
            List<ICrmAction> registeredActions = new List<ICrmAction>();

            RegisterActions(actionFactory, registeredActions);

            foreach (ICrmAction action in registeredActions)
            {
                if (action.CanWork())
                    action.DoWork();
            }
        }
    }
}
