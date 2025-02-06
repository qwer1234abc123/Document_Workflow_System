using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IDocumentIterator
    {
        bool HasNext();
        Document Next();
    }
}
