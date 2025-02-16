using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IDocumentCollection
    {
        // Adds a new document to the collection.
        void AddDocument(Document document);

        // Creates an iterator for traversing the document collection.
        // An optional filter function can be provided to iterate only over specific documents.
        IDocumentIterator CreateIterator(Func<Document, bool> filter = null);
    }

}
