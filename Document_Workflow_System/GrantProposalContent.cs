using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalContent : IContent
    {
        private string contentText;

        public GrantProposalContent()
        {
            contentText = "Grant Proposal Content: Explanation of project goals and funding requirements.";
        }

        public string GetContent()
        {
            return contentText;
        }

        public void SetContent(string newContent)
        {
            contentText = newContent;
        }
    }
}
