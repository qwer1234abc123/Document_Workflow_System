using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportFactory : IDocumentFactory
    {
        public string CreateHeader()
        {
            return "Technical Report Header: Confidential";
        }

        public string CreateFooter()
        {
            return "Technical Report Footer: Prepared by TechCorp";
        }

        public string CreateContent()
        {
            return "Technical Report Content: Detailed technical analysis goes here.";
        }
    }
}
