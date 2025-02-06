using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface INotificationSubject
    {
        void RegisterObserver(INotifiable observer);
        void RemoveObserver(INotifiable observer);
        void NotifyObservers(string message);
    }
}
