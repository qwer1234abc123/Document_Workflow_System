namespace Document_Workflow_System
{
    public class DocumentCollection : IDocumentCollection
    {
        // Private list to store documents in the collection
        private readonly List<Document> documents;

        // Constructor initializes an empty document collection
        public DocumentCollection()
        {
            documents = new List<Document>();
        }

        // Adds a new document to the collection
        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        // Creates an iterator to traverse the document collection
        // The optional filter function allows filtering specific documents (e.g., only approved documents)
        public IDocumentIterator CreateIterator(Func<Document, bool> filter = null)
        {
            return new DocumentIterator(documents, filter);
        }
    }

}
