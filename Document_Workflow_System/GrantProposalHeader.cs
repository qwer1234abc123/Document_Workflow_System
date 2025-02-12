using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalHeader : IHeader
    {
        private string headerText;

        public GrantProposalHeader()
        {
            headerText = "Grant Proposal Header: Funding Request";
        }

        public string GetHeader() => headerText;

        public void SetHeader(string newHeader)
        {
            headerText = newHeader;
        }
    }
}
