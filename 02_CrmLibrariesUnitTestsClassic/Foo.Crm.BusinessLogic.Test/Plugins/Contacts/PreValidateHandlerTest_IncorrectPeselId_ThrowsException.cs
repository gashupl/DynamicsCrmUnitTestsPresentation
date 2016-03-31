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
using Foo.Crm.BusinessLogic.Plugins.Contacts;

namespace Foo.Crm.BusinessLogic.Test.Plugins.Contacts
{
    [TestClass]
    public class PreValidateHandlerTest_IncorrectPeselId_ThrowsException : PluginUnitTest
    {
        protected override IPlugin SetupPlugin()
        {
            //Setup trigger 
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PreValidation);

            Entity contact = new Entity();
            contact.LogicalName = Contact.EntityLogicalName; 
            contact.Attributes.Add(Contact.Fields.foo_idnumber, "123412341234");

            this.SetTarget(contact); 

            //Setup Plugin 
            return new BusinessLogic.Plugins.Contacts.PreValidateHandler();

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPluginExecutionException))]
        public void TestPlugin()
        {
            SetupPlugin(); 
            PreValidateHandler plugin = new PreValidateHandler();
            plugin.Execute(this.ServiceProvider); 
            // base.Test(); //This method is not used in this scenario because it catches exceptions
        }

        protected override void Verify()
        {
           // throw new NotImplementedException();
        }
    }
}
