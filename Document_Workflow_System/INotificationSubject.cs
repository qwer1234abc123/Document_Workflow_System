using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface INotificationSubject
    {
        // Registers an observer to receive notifications.
        void RegisterObserver(INotifiable observer);

        // Removes an observer from the notification list.
        void RemoveObserver(INotifiable observer);

        // Notifies all registered observers with the provided message.
        void NotifyObservers(string message);
    }

}
