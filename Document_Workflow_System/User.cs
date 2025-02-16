using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class User : INotifiable
    {
        // Stores the username of the user, ensuring encapsulation.
        public string Username { get; private set; }

        // Constructor initializes the username upon object creation.
        public User(string username)
        {
            Username = username;
        }

        // Implements the Notify method from INotifiable to send user notifications.
        public void Notify(string message)
        {
            Console.WriteLine($"[Notification for {Username}]: {message}");
        }

        // Overrides ToString() to provide a readable representation of the user object.
        public override string ToString()
        {
            return $"User: {Username}";
        }
    }

}