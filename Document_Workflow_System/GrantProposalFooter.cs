using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalFooter : IFooter
    {
        private string footerText;

        public GrantProposalFooter()
        {
            footerText = "Grant Proposal Footer: Submitted to GrantOrg";
        }

        public string GetFooter() => footerText;

        public void SetFooter(string newFooter)
        {
            footerText = newFooter;
        }
    }
}
