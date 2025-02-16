using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class PDFConversionStrategy : IConversionStrategy
    {
        // Converts the document into a PDF format.
        public string Convert(Document document)
        {
            return $"PDF Document:\nHeader: {document.Header.GetHeader()}\nContent: {document.Content.GetContent()}\nFooter: {document.Footer.GetFooter()}";
        }
    }

}
