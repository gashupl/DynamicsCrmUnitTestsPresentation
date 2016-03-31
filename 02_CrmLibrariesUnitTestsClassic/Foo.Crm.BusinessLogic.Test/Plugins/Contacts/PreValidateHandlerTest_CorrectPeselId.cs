using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm.Framework.Test.Unit.Moq;
using Microsoft.Xrm.Sdk;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Consts;

namespace Foo.Crm.BusinessLogic.Test.Plugins.Contacts
{
    [TestClass]
    public class PreValidateHandlerTest_CorrectPeselId : PluginUnitTest
    {
        protected override IPlugin SetupPlugin()
        {
            //Setup trigger 
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PreValidation);

            Entity contact = new Entity();
            contact.LogicalName = Contact.EntityLogicalName;
            contact.Attributes.Add(Contact.Fields.foo_idnumber, "82051409274");

            this.SetTarget(contact);

            //Setup Plugin 
            return new BusinessLogic.Plugins.Contacts.PreValidateHandler();
        }

        [TestMethod]
        public void TestPlugin()
        {
            base.Test(); 
        }

        protected override void Verify()
        {
            return; 
        }
    }
}
