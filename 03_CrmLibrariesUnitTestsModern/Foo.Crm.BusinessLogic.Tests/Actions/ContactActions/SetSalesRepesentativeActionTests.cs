using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Xrm.Framework.Test.Unit.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Common.Factories;
using Foo.Crm.BusinessLogic.Actions.ContactActions;
using Moq;
using Foo.Crm.BusinessLogic.Services;

namespace Foo.Crm.BusinessLogic.Tests.Actions.ContactActions
{
    [TestClass]
    public class SetSalesRepesentativeActionTests : PluginUnitTest
    {
        [TestMethod]
        public void CanWork_ValidConditions_ReturnsTrue()
        {
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PreOperation);
            this.SetTarget(new Contact() { AccountId = new EntityReference() });

            CrmActionFactory<Entity> factory = new CrmActionFactory<Entity>(this.ServiceProvider, null, null);

            var action = factory.GetAction<SetSalesRepesentativeAction>();

            if (!action.CanWork())
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CanWork_InvalidConditions_ReturnsFalse()
        {
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PostOperation);
            this.SetTarget(new Contact());
            CrmActionFactory<Entity> factory = new CrmActionFactory<Entity>(this.ServiceProvider, null, null);

            var action = factory.GetAction<SetSalesRepesentativeAction>();

            if (action.CanWork())
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DoWork_FindAccountSalesRepresentative_SalesRepSetOnContact()
        {
            Guid expectedId = Guid.NewGuid(); 

            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PreOperation);
            this.SetTarget(new Contact() { AccountId = new EntityReference() });

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetById(It.IsAny<Guid>()))
                .Returns(new Account() { foo_salesrepresentativeid = new EntityReference("systemuser", expectedId) }); 

            Mock<ICrmUnitOfWorkService> crmServiceUowMock = new Mock<ICrmUnitOfWorkService>();
            crmServiceUowMock.Setup(service => service.AccountService).Returns(accountService.Object); 

            CrmActionFactory<Entity> factory = new CrmActionFactory<Entity>(this.ServiceProvider, null, crmServiceUowMock.Object);

            var action = factory.GetAction<SetSalesRepesentativeAction>();

            action.DoWork();

            Contact contact = this.GetTarget() as Contact;

            Assert.IsNotNull(contact);
            Assert.IsNotNull(contact.foo_salesrepresentativeid);
            Assert.AreEqual(expectedId, contact.foo_salesrepresentativeid.Id); 
        }

        protected override IPlugin SetupPlugin()
        {
            throw new NotImplementedException();
        }

        protected override void Verify()
        {
            throw new NotImplementedException();
        }
    }
}
