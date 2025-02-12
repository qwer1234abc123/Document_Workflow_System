using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IContent
    {
        string GetContent();
        void SetContent(string newContent);
    }

}

