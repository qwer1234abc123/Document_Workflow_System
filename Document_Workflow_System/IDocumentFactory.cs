using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IDocumentFactory
    {
        // Creates a document header component.
        IHeader CreateHeader();

        // Creates a document footer component.
        IFooter CreateFooter();

        // Creates the main content section of the document.
        IContent CreateContent();

        // Creates an additional component (e.g., appendix, budget section).
        IAdditionalComponent CreateAdditionalComponent();
    }

}
