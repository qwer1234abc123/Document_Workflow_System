using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class User : INotifiable
    {
        public string Username { get; private set; }

        public User(string username)
        {
            Username = username;
        }

        public void Notify(string message)
        {
            Console.WriteLine($"[Notification for {Username}]: {message}");
        }

        public override string ToString()
        {
            return $"User: {Username}";
        }
    }
}