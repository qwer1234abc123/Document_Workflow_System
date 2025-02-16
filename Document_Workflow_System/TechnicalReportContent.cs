using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportContent : IContent
    {
        // Stores the content of the technical report.
        private string contentText;

        // Initializes the technical report with default content.
        public TechnicalReportContent()
        {
            contentText = "Technical Report Content: Detailed AI Research Analysis.";
        }

        // Retrieves the current content of the technical report.
        public string GetContent()
        {
            return contentText;
        }

        // Updates the content of the technical report with new text.
        public void SetContent(string newContent)
        {
            contentText = newContent;
        }
    }


}
