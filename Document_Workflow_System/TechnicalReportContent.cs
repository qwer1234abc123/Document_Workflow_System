using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportContent : IContent
    {
        private string contentText;

        public TechnicalReportContent()
        {
            contentText = "Technical Report Content: Detailed AI Research Analysis.";
        }

        public string GetContent()
        {
            return contentText;
        }

        public void SetContent(string newContent)
        {
            contentText = newContent;
        }
    }

}
