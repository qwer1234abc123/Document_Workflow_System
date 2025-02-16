using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IAdditionalComponent
    {
        // Retrieves the additional component details specific to the document type
        string GetAdditionalComponent();
    }

}
