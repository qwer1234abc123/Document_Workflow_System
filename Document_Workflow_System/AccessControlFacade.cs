using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class AccessControlFacade
    {
        // Check if a user can be added as a collaborator
        public bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver)
        {
            // Rule 1: User cannot be the owner of the document
            if (user == owner)
                return false;

            // Rule 2: User cannot already be a collaborator
            if (collaborators.Contains(user))
                return false;

            // Rule 3: User cannot be the current approver
            if (user == approver)
                return false;

            // Rule 4: Any other user is eligible to be a collaborator
            return true;
        }


        // Check if a user can be an approver
        public bool CanBeApprover(User user, List<User> collaborators, User owner)
        {
            // User cannot be the owner or a collaborator in the current document
            if (user == owner || collaborators.Contains(user))
                return false;

            // The user must have the "Approver" role
            return true;
        }

        public bool CanSubmitForApproval(Document document, User approver)
        {
            // Rule 1: Document must be in DraftState or RejectedState
            if (document.State.GetType().Name != "DraftState" && document.State.GetType().Name != "RejectedState")
                return false;

            // Rule 2: Approver must be valid
            return CanBeApprover(approver, document.Collaborators, document.Owner);
        }


        // Check if a user can edit the document
        public bool CanEditDocument(User user, User owner, List<User> collaborators, string state)
        {
            if (state == "UnderReviewState" || state == "ApprovedState")
            {
                return false; // No edits allowed in these states
            }

            return user == owner || collaborators.Contains(user);
        }
        public bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state)
        {
            // Allow access to the owner and collaborators
            if (user == owner || collaborators.Contains(user)) return true;

            // Allow access to the approver in 'UnderReviewState' and 'PushedBackState'
            if (user == approver && (state == "UnderReviewState" || state == "PushedBackState" || state == "ApprovedState")) return true;

            // Otherwise, deny access
            return false;
        }


    }
}
