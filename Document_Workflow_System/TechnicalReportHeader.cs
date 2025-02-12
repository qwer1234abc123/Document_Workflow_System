using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportHeader : IHeader
    {
        private string headerText;

        public TechnicalReportHeader()
        {
            headerText = "Technical Report Header: Confidential";
        }

        public string GetHeader() => headerText;

        public void SetHeader(string newHeader) 
        {
            headerText = newHeader;
        }
    }

}
