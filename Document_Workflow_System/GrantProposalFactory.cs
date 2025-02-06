using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalFactory : IDocumentFactory
    {
        public string CreateHeader()
        {
            return "Grant Proposal Header: Funding Request";
        }

        public string CreateFooter()
        {
            return "Grant Proposal Footer: Submitted to GrantOrg";
        }

        public string CreateContent()
        {
            return "Grant Proposal Content: Explanation of project goals and funding requirements.";
        }
    }
}
