using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class RealAccessControl : IAccessControl
    {
        public bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver)
        {
            if (user == owner || collaborators.Contains(user) || user == approver)
                return false;
            return true;
        }

        public bool CanBeApprover(User user, List<User> collaborators, User owner)
        {
            return user != owner && !collaborators.Contains(user);
        }

        public bool CanSubmitForApproval(Document document, User approver)
        {
            string state = document.State.GetType().Name;
            return (state == "DraftState" || state == "RejectedState") && CanBeApprover(approver, document.Collaborators, document.Owner);
        }

        public bool CanEditDocument(User user, User owner, List<User> collaborators, string state)
        {
            return (state != "UnderReviewState" && state != "ApprovedState") && (user == owner || collaborators.Contains(user));
        }

        public bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state)
        {
            return user == owner || collaborators.Contains(user) ||
                   (user == approver && (state == "UnderReviewState" || state == "PushedBackState" || state == "ApprovedState"));
        }
    }
}
