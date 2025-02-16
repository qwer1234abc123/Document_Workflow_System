using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportFooter : IFooter
    {
        private string footerText; // Stores the footer content for the technical report.

        // Constructor initializes the default footer text.
        public TechnicalReportFooter()
        {
            footerText = "Technical Report Footer: Prepared by TechCorp";
        }

        // Retrieves the current footer text.
        public string GetFooter() => footerText;

        // Updates the footer text with new content.
        public void SetFooter(string newFooter)
        {
            footerText = newFooter;
        }
    }

}
