using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportFactory : IDocumentFactory
    {
        // Creates and returns a header for the technical report.
        public IHeader CreateHeader() => new TechnicalReportHeader();

        // Creates and returns a footer for the technical report.
        public IFooter CreateFooter() => new TechnicalReportFooter();

        // Creates and returns content for the technical report.
        public IContent CreateContent() => new TechnicalReportContent();

        // Creates and returns an additional component (appendix) for the technical report.
        public IAdditionalComponent CreateAdditionalComponent() => new TechnicalReportAppendix();
    }

}
