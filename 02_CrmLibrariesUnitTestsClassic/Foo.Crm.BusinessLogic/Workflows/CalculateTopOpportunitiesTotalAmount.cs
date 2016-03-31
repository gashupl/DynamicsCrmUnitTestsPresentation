using System;
using System.Activities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using Foo.Crm.BusinessLogic.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Foo.Crm.BusinessLogic.Repositories;

namespace Foo.Crm.BusinessLogic.Workflows
{
    public class CalculateTopOpportunitiesTotalAmount : CodeActivity
    {

        protected IOpportunityRepository crmRepository;

        #region Constructors
        public CalculateTopOpportunitiesTotalAmount(IOpportunityRepository crmRepository)
        {
            this.crmRepository = crmRepository; 
        }

        public CalculateTopOpportunitiesTotalAmount()
        {

        }
        #endregion

        [Input("Ilość szans sprzedaży")]
        public InArgument<int> OpportunitiesTopCount
        {
            get;
            set;
        }

        [Output("Suma szans sprzedaży")]
        public OutArgument<decimal> TopOpportunitiesTotalAmount
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext wfContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService orgService = serviceFactory.CreateOrganizationService(wfContext.UserId);
            this.InitializeRepository(orgService); 

            try
            {
                int opportunityNumber = context.GetValue(this.OpportunitiesTopCount);

                Guid accountId = wfContext.PrimaryEntityId;

                var amounts = this.crmRepository.GetOpportunitiesTotalAmount(accountId); 

                List<Money> amountsList = amounts.ToList<Money>()
                    .OrderByDescending(a => a.Value).
                    Take<Money>(opportunityNumber)
                    .ToList<Money>();

                decimal totalAmount = 0;       
                foreach (Money amount in amountsList)
                {
                    totalAmount += amount.Value;

                }

                context.SetValue(this.TopOpportunitiesTotalAmount, totalAmount);

            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                throw new InvalidPluginExecutionException("CalculateTopOpportunitiesTotalAmount Exception", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("CalculateTopOpportunitiesTotalAmount Exception", ex);

            }
        }

        private void InitializeRepository(IOrganizationService orgService)
        {
            if (this.crmRepository == null)
            {
                this.crmRepository = new OpportunityRepository(orgService);
            }
        }
    }
}
