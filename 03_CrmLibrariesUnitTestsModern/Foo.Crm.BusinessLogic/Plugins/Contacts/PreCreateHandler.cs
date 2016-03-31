using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using Foo.Crm.BusinessLogic.Actions;
using Foo.Crm.BusinessLogic.Common.Factories;
using System.Collections.Generic;
using Foo.Crm.BusinessLogic.Actions.ContactActions;

namespace Foo.Crm.BusinessLogic.Plugins.Contacts
{
    public class PreCreateHandler : PluginBase<Contact>, IPlugin
    {

        public override void RegisterActions(CrmActionFactory<Contact> actionFactory, List<ICrmAction> registeredActions)
        {
            registeredActions.Add(actionFactory.GetAction<SetSalesRepesentativeAction>());
            
        }
    }
}
