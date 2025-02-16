using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class AccessControlProxy : IAccessControlSubject
    {
        private readonly AccessControlRealSubject _realAccessControl;
        private readonly Dictionary<string, DateTime> _requestTimestamps; // Rate-limit tracking

        public AccessControlProxy()
        {
            _realAccessControl = new AccessControlRealSubject();
            _requestTimestamps = new Dictionary<string, DateTime>();
        }

        // **Helper method: Rate Limiting**
        private bool IsRateLimited(string key)
        {
            if (_requestTimestamps.ContainsKey(key) &&
                (DateTime.Now - _requestTimestamps[key]).TotalSeconds < 5)
            {
                Console.WriteLine($"Rate Limit Exceeded: Too many requests for {key}. Try again later.");
                return true;
            }

            _requestTimestamps[key] = DateTime.Now; // Update last request time
            return false;
        }

        public bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver, string state)
        {
            string key = $"AddCollab-{user.Username}";
            if (IsRateLimited(key)) return false;

            return _realAccessControl.CanAddCollaborator(user, collaborators, owner, approver, state);
        }

        public bool CanBeApprover(User user, List<User> collaborators, User owner)
        {
            string key = $"BeApprover-{user.Username}";
            if (IsRateLimited(key)) return false;

            return _realAccessControl.CanBeApprover(user, collaborators, owner);
        }

        public bool CanSubmitForApproval(Document document, User approver)
        {
            string key = $"SubmitApproval-{approver.Username}";
            if (IsRateLimited(key)) return false;

            return _realAccessControl.CanSubmitForApproval(document, approver);
        }

        public bool CanEditDocument(User user, User owner, List<User> collaborators, string state)
        {
            string key = $"EditDoc-{user.Username}";
            if (IsRateLimited(key)) return false;

            return _realAccessControl.CanEditDocument(user, owner, collaborators, state);
        }

        public bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state)
        {
            string key = $"AccessDoc-{user.Username}";
            if (IsRateLimited(key)) return false;

            return _realAccessControl.CanAccessDocument(user, owner, collaborators, approver, state);
        }
    }
}
