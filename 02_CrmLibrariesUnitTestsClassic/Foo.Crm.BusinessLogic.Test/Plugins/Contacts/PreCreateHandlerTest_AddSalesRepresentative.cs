using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Consts;
using Xrm.Framework.Test.Unit.Moq;
using Moq;
using Microsoft.Xrm.Sdk.Query;

namespace Foo.Crm.BusinessLogic.Test.Plugins.Contacts
{
    [TestClass]
    public class PreCreateHandlerTest_AddSalesRepresentative : PluginUnitTest
    {
        private Guid expectedGuid = Guid.NewGuid(); 
        protected override IPlugin SetupPlugin()
        {
            //Setup trigger 
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PreOperation);
    
            Entity contact = new Entity();
            contact.LogicalName = Contact.EntityLogicalName;
            contact.Attributes.Add(Contact.Fields.foo_idnumber, "82051409274");
            contact.Attributes.Add(Contact.Fields.AccountId, new EntityReference(Account.EntityLogicalName, Guid.NewGuid()));

            Entity account = new Entity();
            account.LogicalName = Account.EntityLogicalName;
            account.Attributes.Add(Account.Fields.foo_salesrepresentativeid, new EntityReference(SystemUser.EntityLogicalName, this.expectedGuid)); 

            this.SetTarget(contact);

            this.OrganizationServiceMock
                .Setup(oService => oService.Retrieve(Account.EntityLogicalName, It.IsAny<Guid>(), It.IsAny<ColumnSet>()))
                .Returns(account); 

            //Setup Plugin 
            return new BusinessLogic.Plugins.Contacts.PreCreateHandler();
        }

        [TestMethod]
        public void TestPlugin()
        {
            base.Test(); 
        }

        protected override void Verify()
        {
            Entity contact = this.GetTarget() as Entity;

            if (!contact.Attributes.Contains(Contact.Fields.foo_salesrepresentativeid))
            {
                Assert.Fail("Brak informacji o reprezentancie handlowym"); 
            }

            EntityReference salesRepresentativeRef = contact.Attributes[Contact.Fields.foo_salesrepresentativeid] as EntityReference;
            Assert.AreEqual(this.expectedGuid, salesRepresentativeRef.Id); 
             
        }
    }
}
