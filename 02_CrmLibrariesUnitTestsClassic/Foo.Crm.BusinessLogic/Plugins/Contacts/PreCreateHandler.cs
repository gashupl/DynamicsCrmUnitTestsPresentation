using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Foo.Crm.BusinessLogic.Plugins.Contacts
{
    public class PreCreateHandler : IPlugin
    {
        /// <summary>
        /// Pobranie danych innych klienta i skopiowanie inicjalnej wartości reprezentanta handlowego na kontakt
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains(CrmConst.Target) && context.InputParameters[CrmConst.Target] is Entity)
            {
                Entity target = (Entity)context.InputParameters[CrmConst.Target];

                if (target.Attributes.Contains(Contact.Fields.AccountId))
                {
                    EntityReference accountRef = target.Attributes[Contact.Fields.AccountId] as EntityReference; 

                    if(accountRef != null)
                    {
                        Entity parentAccount = 
                            service.Retrieve(Account.EntityLogicalName, accountRef.Id, new ColumnSet(new string[]{ Account.Fields.foo_salesrepresentativeid }));

                        if (parentAccount.Attributes.Contains(Account.Fields.foo_salesrepresentativeid))
                        {
                            EntityReference salesRepresentativeRef = parentAccount.Attributes[Account.Fields.foo_salesrepresentativeid] as EntityReference;
                            target.Attributes.Add(Contact.Fields.foo_salesrepresentativeid, salesRepresentativeRef); 
                        }

                    }
                }
            }
        }
    }
}
