using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalFactory : IDocumentFactory
    {
        // Creates and returns a header specific to a grant proposal
        public IHeader CreateHeader() => new GrantProposalHeader();

        // Creates and returns a footer specific to a grant proposal
        public IFooter CreateFooter() => new GrantProposalFooter();

        // Creates and returns the content section for a grant proposal
        public IContent CreateContent() => new GrantProposalContent();

        // Creates and returns the additional component (budget section) for a grant proposal
        public IAdditionalComponent CreateAdditionalComponent() => new GrantProposalBudget();
    }


}
