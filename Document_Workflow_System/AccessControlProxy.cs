﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class AccessControlProxy : IAccessControlSubject
    {
        private readonly AccessControlRealSubject _realAccessControl;
        private readonly Dictionary<string, bool> _cache = new Dictionary<string, bool>(); // Caching results

        public AccessControlProxy()
        {
            _realAccessControl = new AccessControlRealSubject();
        }

        public bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver, string state)
        {
            string key = $"AddCollab-{user.Username}-{owner.Username}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanAddCollaborator(user, collaborators, owner, approver, state);
            }
            return _cache[key];
        }

        public bool CanBeApprover(User user, List<User> collaborators, User owner)
        {
            string key = $"BeApprover-{user.Username}-{owner.Username}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanBeApprover(user, collaborators, owner);
            }
            return _cache[key];
        }

        public bool CanSubmitForApproval(Document document, User approver)
        {
            string key = $"Submit-{document.Header}-{approver.Username}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanSubmitForApproval(document, approver);
            }
            return _cache[key];
        }

        public bool CanEditDocument(User user, User owner, List<User> collaborators, string state)
        {
            string key = $"Edit-{user.Username}-{state}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanEditDocument(user, owner, collaborators, state);
            }
            return _cache[key];
        }

        public bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state)
        {
            string key = $"Access-{user.Username}-{state}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _realAccessControl.CanAccessDocument(user, owner, collaborators, approver, state);
            }
            return _cache[key];
        }
    }
}
