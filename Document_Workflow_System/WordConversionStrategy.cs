using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class WordConversionStrategy : IConversionStrategy
    {
        public string Convert(Document document)
        {
            return $"Word Document:\nHeader: {document.Header}\nContent: {document.Content}\nFooter: {document.Footer}";
        }
    }
}
