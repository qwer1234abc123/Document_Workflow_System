﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface INotifiable
    {
        // Sends a notification message to the implementing entity.
        void Notify(string message);
    }

}
