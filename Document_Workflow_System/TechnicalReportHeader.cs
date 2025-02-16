using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportHeader : IHeader
    {
        private string headerText; // Stores the header content for the technical report.

        // Constructor initializes the default header text.
        public TechnicalReportHeader()
        {
            headerText = "Technical Report Header: Confidential";
        }

        // Retrieves the current header text.
        public string GetHeader() => headerText;

        // Updates the header text with new content.
        public void SetHeader(string newHeader)
        {
            headerText = newHeader;
        }
    }


}
