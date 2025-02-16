using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class TechnicalReportAppendix : IAdditionalComponent
    {
        // Returns the additional component details for a technical report.
        // In this case, it provides information about the appendix section.
        public string GetAdditionalComponent() => "Appendix: AI Research Data";
    }

}
