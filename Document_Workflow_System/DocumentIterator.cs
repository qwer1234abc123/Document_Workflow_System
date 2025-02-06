using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class DocumentIterator : IDocumentIterator
    {
        private readonly List<Document> documents;
        private readonly Func<Document, bool> filter; // Filtering logic
        private int position = 0;

        public DocumentIterator(List<Document> documents, Func<Document, bool> filter = null)
        {
            this.documents = documents;
            this.filter = filter ?? (_ => true); // Default to no filtering
        }

        public bool HasNext()
        {
            while (position < documents.Count)
            {
                if (filter(documents[position])) return true;
                position++;
            }
            return false;
        }

        public Document Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more documents.");
            }
            return documents[position++];
        }
    }
}
