using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportFactory : IDocumentFactory
    {
        public IHeader CreateHeader() => new TechnicalReportHeader();
        public IFooter CreateFooter() => new TechnicalReportFooter();
        public IContent CreateContent() => new TechnicalReportContent();
        public IAdditionalComponent CreateAdditionalComponent() => new TechnicalReportAppendix();
    }
}
