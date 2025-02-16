using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class AccessControlRealSubject : IAccessControlSubject
    {
        // Determines if a user can be added as a collaborator based on document state and existing roles
        public bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver, string state)
        {
            // Rule 1: Collaborators cannot be added if the document is in the RejectedState
            if (state == "RejectedState")
            {
                Console.WriteLine("Error: Cannot add collaborators while the document is in Rejected State.");
                return false;
            }

            // Rule 2: A user cannot be added as a collaborator if they are already the owner, an existing collaborator, or the approver
            if (user == owner || collaborators.Contains(user) || user == approver)
            {
                return false;
            }

            return true;
        }

        // Determines if a user can be assigned as the document approver
        public bool CanBeApprover(User user, List<User> collaborators, User owner)
        {
            // The approver must not be the document owner or an existing collaborator
            return user != owner && !collaborators.Contains(user);
        }

        // Determines if a document can be submitted for approval by a given approver
        public bool CanSubmitForApproval(Document document, User approver)
        {
            string state = document.State.GetType().Name;

            // A document can only be submitted if it is in Draft or Rejected state and the approver is valid
            return (state == "DraftState" || state == "RejectedState") &&
                   CanBeApprover(approver, document.Collaborators, document.Owner);
        }

        // Determines if a user is allowed to edit a document based on its current state and ownership
        public bool CanEditDocument(User user, User owner, List<User> collaborators, string state)
        {
            // Editing is not allowed if the document is in UnderReview or Approved state
            return (state != "UnderReviewState" && state != "ApprovedState") &&
                   (user == owner || collaborators.Contains(user));
        }

        // Determines if a user has access to a document based on their role and the document’s state
        public bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state)
        {
            // A user has access if they are the owner, a collaborator, or the approver under specific states
            return user == owner || collaborators.Contains(user) ||
                   (user == approver && (state == "UnderReviewState" || state == "PushedBackState" || state == "ApprovedState"));
        }
    }

}
