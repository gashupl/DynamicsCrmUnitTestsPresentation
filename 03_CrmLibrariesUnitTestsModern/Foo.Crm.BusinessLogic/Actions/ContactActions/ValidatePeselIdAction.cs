using Foo.Crm.BusinessLogic.Common.Consts;
using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Validators;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Actions.ContactActions
{
    public class ValidatePeselIdAction : CrmActionBase
    {
        public override bool CanWork()
        {
            Contact target = this.TargetEntity.ToEntity<Contact>();

            return this.Context.MessageName == CrmMessagesNames.Create
                && this.Context.Stage == (int)PluginExecutionStageEnum.PreOperation; 
        }

        public override void DoWork()
        {

            Contact target = this.TargetEntity.ToEntity<Contact>();

            if(target.foo_idnumber == null || !(new PeselValidator(target.foo_idnumber)).IsValid())
            {
                throw new InvalidPluginExecutionException(CrmMessages.ContactMessages.IncorrectOrEmptyPesel);
            }

        }
    }
}
