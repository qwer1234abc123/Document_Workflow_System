using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalHeader : IHeader
    {
        private string headerText; // Stores the header text specific to a grant proposal

        // Initializes the header with a default value
        public GrantProposalHeader()
        {
            headerText = "Grant Proposal Header: Funding Request";
        }

        // Retrieves the current header text
        public string GetHeader() => headerText;

        // Updates the header text with a new value
        public void SetHeader(string newHeader)
        {
            headerText = newHeader;
        }
    }

}
