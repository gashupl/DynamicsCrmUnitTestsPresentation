using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public interface IOpportunityRepository : ICrmRepositoryBase
    {
        IQueryable<Money> GetOpportunitiesTotalAmount(Guid accountId); 
    }
}
