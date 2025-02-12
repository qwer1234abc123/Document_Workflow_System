using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportFooter : IFooter
    {
        private string footerText;

        public TechnicalReportFooter()
        {
            footerText = "Technical Report Footer: Prepared by TechCorp";
        }

        public string GetFooter() => footerText;

        public void SetFooter(string newFooter)
        {
            footerText = newFooter;
        }
    }
}
