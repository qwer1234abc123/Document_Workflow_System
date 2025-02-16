using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalBudget : IAdditionalComponent
    {
        // Returns the budget breakdown details for the grant proposal
        public string GetAdditionalComponent() => "Budget Breakdown Section: Financial Plan and Costs";
    }

}
