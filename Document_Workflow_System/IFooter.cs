using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IFooter
    {
        // Retrieves the current footer content.
        string GetFooter();

        // Updates the footer content with a new value.
        void SetFooter(string footerText);
    }

}
