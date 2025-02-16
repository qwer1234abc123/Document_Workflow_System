using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IContent
    {
        // Retrieves the content of the document
        string GetContent();

        // Updates the content of the document with new text
        void SetContent(string newContent);
    }


}

