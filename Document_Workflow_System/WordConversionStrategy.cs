using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class WordConversionStrategy : IConversionStrategy
    {
        // Converts a document into a Word-formatted string representation.
        public string Convert(Document document)
        {
            return $"Word Document:\nHeader: {document.Header.GetHeader()}\nContent: {document.Content.GetContent()}\nFooter: {document.Footer.GetFooter()}";
        }
    }

}
