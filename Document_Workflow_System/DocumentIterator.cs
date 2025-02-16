using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class DocumentIterator : IDocumentIterator
    {
        // Readonly list of documents to iterate over
        private readonly List<Document> documents;

        // Function delegate for filtering documents during iteration
        private readonly Func<Document, bool> filter;

        // Tracks the current position in the document list
        private int position = 0;

        // Constructor initializes the iterator with a list of documents and an optional filter function
        public DocumentIterator(List<Document> documents, Func<Document, bool> filter = null)
        {
            this.documents = documents;
            this.filter = filter ?? (_ => true); // Default filter accepts all documents
        }

        // Checks if there is a next document that matches the filter condition
        public bool HasNext()
        {
            while (position < documents.Count)
            {
                if (filter(documents[position])) return true;
                position++; // Skip non-matching documents
            }
            return false;
        }

        // Retrieves the next document that matches the filter condition
        public Document Next()
        {
            if (!HasNext()) // Ensure there is a next document before proceeding
            {
                throw new InvalidOperationException("No more documents.");
            }
            return documents[position++]; // Return the next valid document and advance position
        }
    }

}
