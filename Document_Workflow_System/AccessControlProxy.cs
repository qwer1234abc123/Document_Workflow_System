using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class AccessControlProxy : IAccessControlSubject
    {
        // The real access control subject that handles actual permission checks
        private readonly AccessControlRealSubject _realAccessControl;

        // A cache to store access control decisions, reducing redundant checks
        private readonly Dictionary<string, bool> _cache = new Dictionary<string, bool>();

        public AccessControlProxy()
        {
            _realAccessControl = new AccessControlRealSubject();
        }

        // Checks if a user can be added as a collaborator, caching results for efficiency
        public bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver, string state)
        {
            string key = $"AddCollab-{user.Username}-{owner.Username}";

            // If the result is not in the cache, compute and store it
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanAddCollaborator(user, collaborators, owner, approver, state);
            }

            return _cache[key];
        }

        // Determines if a user can be assigned as an approver for a document
        public bool CanBeApprover(User user, List<User> collaborators, User owner)
        {
            string key = $"BeApprover-{user.Username}-{owner.Username}";

            // If the result is not cached, retrieve and store it
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanBeApprover(user, collaborators, owner);
            }

            return _cache[key];
        }

        // Checks if a document can be submitted for approval by a given approver
        public bool CanSubmitForApproval(Document document, User approver)
        {
            string key = $"Submit-{document.Header}-{approver.Username}";

            // If not cached, retrieve and store the result
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanSubmitForApproval(document, approver);
            }

            return _cache[key];
        }

        // Determines whether a user can edit the document in its current state
        public bool CanEditDocument(User user, User owner, List<User> collaborators, string state)
        {
            string key = $"Edit-{user.Username}-{state}";

            // If the edit permission is not cached, check and store it
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanEditDocument(user, owner, collaborators, state);
            }

            return _cache[key];
        }

        // Checks if a user has access to a document based on ownership, collaboration, or approval role
        public bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state)
        {
            string key = $"Access-{user.Username}-{state}";

            // If the access check result is not cached, retrieve and store it
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanAccessDocument(user, owner, collaborators, approver, state);
            }

            return _cache[key];
        }
    }

}
