using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Validators;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foo.Crm.BusinessLogic.Actions;
using Foo.Crm.BusinessLogic.Common.Factories;
using Foo.Crm.BusinessLogic.Actions.ContactActions;

namespace Foo.Crm.BusinessLogic.Plugins.Contacts
{
    public class PreValidateHandler : PluginBase<Contact>, IPlugin
    {

        public override void RegisterActions(CrmActionFactory<Contact> actionFactory, List<ICrmAction> registeredActions)
        {
            registeredActions.Add(actionFactory.GetAction<ValidatePeselIdAction>());
        }
    }
}
