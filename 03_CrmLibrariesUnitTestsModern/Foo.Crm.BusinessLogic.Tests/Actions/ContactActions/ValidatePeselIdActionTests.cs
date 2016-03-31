using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Xrm.Framework.Test.Unit.Moq;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Common.Factories;
using Foo.Crm.BusinessLogic.Actions.ContactActions;

namespace Foo.Crm.BusinessLogic.Test.Actions.ContactActions
{
    [TestClass]
    public class ValidatePeselIdActionTests : PluginUnitTest
    {
        [TestMethod]
        public void CanWork_ValidConditions_ReturnsTrue()
        {
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PreOperation);
            this.SetTarget(new Contact());

            CrmActionFactory<Entity> factory = new CrmActionFactory<Entity>(this.ServiceProvider, null, null);

            var action = factory.GetAction<ValidatePeselIdAction>();

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

            var action = factory.GetAction<ValidatePeselIdAction>();

            if (action.CanWork())
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DoWork_ValidPesel()
        {
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PostOperation);
            this.SetTarget(new Contact()
            {
                foo_idnumber = "82051409274" 
            }); 

            CrmActionFactory<Entity> factory = new CrmActionFactory<Entity>(this.ServiceProvider, null, null);

            var action = factory.GetAction<ValidatePeselIdAction>();

            action.DoWork(); 
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPluginExecutionException))]
        public void DoWork_InvalidPesel_ThrowsException()
        {
            this.SetPluginEvent(Contact.EntityLogicalName, CrmMessagesNames.Create, Xrm.Framework.Test.Unit.SdkMessageProcessingStepImage.PostOperation);
            this.SetTarget(new Contact()
            {
                foo_idnumber = "666666"
            });

            CrmActionFactory<Entity> factory = new CrmActionFactory<Entity>(this.ServiceProvider, null, null);

            var action = factory.GetAction<ValidatePeselIdAction>();

            action.DoWork();
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
