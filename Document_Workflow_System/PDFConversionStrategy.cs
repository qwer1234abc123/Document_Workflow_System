using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class PDFConversionStrategy : IConversionStrategy
    {
        public string Convert(Document document)
        {
            return $"PDF Document:\nHeader: {document.Header}\nContent: {document.Content}\nFooter: {document.Footer}";
        }
    }
}
