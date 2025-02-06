using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class DocumentCollection
    {
        private readonly List<Document> documents;

        public DocumentCollection()
        {
            documents = new List<Document>();
        }

        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        public IDocumentIterator CreateIterator(Func<Document, bool> filter = null)
        {
            return new DocumentIterator(documents, filter);
        }
    }

}
