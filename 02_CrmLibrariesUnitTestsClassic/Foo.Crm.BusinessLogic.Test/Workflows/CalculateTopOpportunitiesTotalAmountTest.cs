using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm.Framework.Test.Unit.Moq;
using System.Activities;
using Moq;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Workflows;
using Foo.Crm.BusinessLogic.Repositories;

namespace Foo.Crm.BusinessLogic.Test.Workflows
{
    [TestClass]
    public class CalculateTopOpportunitiesTotalAmountTest : WFActivityUnitTest
    {
        protected override Activity SetupActivity()
        {

            List<Money> reponseCollection = new List<Money>(); 
            reponseCollection.Add(new Money(10));
            reponseCollection.Add(new Money(20));
            reponseCollection.Add(new Money(30));

            Mock<IOpportunityRepository> crmRepository = new Mock<IOpportunityRepository>();
            crmRepository.Setup(repo => repo.GetOpportunitiesTotalAmount(It.IsAny<Guid>())).Returns(reponseCollection.AsQueryable<Money>()); 

            base.Inputs.Add("OpportunitiesTopCount", 2);

            Activity activity = new CalculateTopOpportunitiesTotalAmount(crmRepository.Object); 

            return activity;
        }

        [TestMethod]
        public void TestWorkFlow()
        {
            base.Test(); 
        }

        protected override void Verify()
        {
            decimal expectedAmount = 50;

            Assert.IsNull(Error);

            decimal amount = (decimal)base.Outputs["TopOpportunitiesTotalAmount"];

            Assert.AreEqual(expectedAmount, amount);
        }
    }
}
