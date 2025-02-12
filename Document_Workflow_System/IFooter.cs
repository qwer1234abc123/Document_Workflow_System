using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IFooter
    {
        string GetFooter();
        void SetFooter(string footerText); 
    }
}
