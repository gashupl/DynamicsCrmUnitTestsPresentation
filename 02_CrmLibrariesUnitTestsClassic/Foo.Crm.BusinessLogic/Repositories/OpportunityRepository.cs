using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Foo.Crm.BusinessLogic.Model;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        XrmServiceContext xrmService;

        public OpportunityRepository(IOrganizationService orgService)
        {
            this.xrmService = new XrmServiceContext(orgService);
        }

        public IQueryable<Money> GetOpportunitiesTotalAmount(Guid accountId)
        {
            var amounts = (from o in xrmService.OpportunitySet
                           where o.CustomerId.Id == accountId
                           select o.TotalAmount);

            return amounts; 
        }
    }
}
