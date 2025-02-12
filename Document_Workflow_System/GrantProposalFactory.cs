using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class GrantProposalFactory : IDocumentFactory
    {
        public IHeader CreateHeader() => new GrantProposalHeader();
        public IFooter CreateFooter() => new GrantProposalFooter();
        public IContent CreateContent() => new GrantProposalContent();
        public IAdditionalComponent CreateAdditionalComponent() => new GrantProposalBudget();
    }

}
