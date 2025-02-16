using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalContent : IContent
    {
        private string contentText; // Stores the content text of the grant proposal

        // Constructor initializes the default content text for the grant proposal
        public GrantProposalContent()
        {
            contentText = "Grant Proposal Content: Explanation of project goals and funding requirements.";
        }

        // Retrieves the current content of the grant proposal
        public string GetContent()
        {
            return contentText;
        }

        // Updates the content of the grant proposal with new text
        public void SetContent(string newContent)
        {
            contentText = newContent;
        }
    }

}
