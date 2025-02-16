using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IHeader
    {
        // Retrieves the current header content.
        string GetHeader();

        // Updates the header content with a new value.
        void SetHeader(string headerText);
    }

}
