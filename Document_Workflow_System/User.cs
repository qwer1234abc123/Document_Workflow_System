using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public enum UserRole
    {
        Owner,
        Collaborator,
        Approver
    }

    public class User : INotifiable
    {
        public string Username { get; private set; }
        public UserRole Role { get; private set; }

        public User(string username, UserRole role)
        {
            Username = username;
            Role = role;
        }

        public void AssignRole(UserRole role)
        {
            Role = role;
        }

        public bool CanPerformAction(string action)
        {
            if (Role == UserRole.Approver && action == "Edit")
            {
                return false;
            }
            return true;
        }

        public void Notify(string message)
        {
            Console.WriteLine($"[Notification for {Username}]: {message}");
        }

        public override string ToString()
        {
            return $"User: {Username}, Role: {Role}";
        }
    }
}