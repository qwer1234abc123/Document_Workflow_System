using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IConversionStrategy
    {
        // Converts the document into a specified format (e.g., PDF, Word)
        string Convert(Document document);
    }

}
