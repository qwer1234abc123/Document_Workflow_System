using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalFooter : IFooter
    {
        private string footerText; // Stores the footer text specific to a grant proposal

        // Initializes the footer with a default value
        public GrantProposalFooter()
        {
            footerText = "Grant Proposal Footer: Submitted to GrantOrg";
        }

        // Retrieves the current footer text
        public string GetFooter() => footerText;

        // Updates the footer text with a new value
        public void SetFooter(string newFooter)
        {
            footerText = newFooter;
        }
    }

}
