using Foo.Crm.BusinessLogic.Common.Consts;
using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Actions.ContactActions
{
    public class SetSalesRepesentativeAction : CrmActionBase
    {
        public override bool CanWork()
        {
            Contact target = this.TargetEntity.ToEntity<Contact>();

            return this.Context.MessageName == CrmMessagesNames.Create
                && this.Context.Stage == (int)PluginExecutionStageEnum.PreOperation
                && target.AccountId != null; 
        }

        public override void DoWork()
        {
            Contact target = this.TargetEntity.ToEntity<Contact>();
            Account parentAccount = this.CrmUnitOfWorkService.AccountService.GetById(target.AccountId.Id);

            target.foo_salesrepresentativeid = parentAccount.foo_salesrepresentativeid; 

        }
    }
}
