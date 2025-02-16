using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IDocumentIterator
    {
        // Checks if there is another document in the collection.
        bool HasNext();

        // Retrieves the next document in the collection.
        Document Next();
    }

}
