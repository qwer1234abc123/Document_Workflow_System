using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IAccessControlSubject
    {
        // Determines if a user can be added as a collaborator based on document state and existing roles
        bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver, string state);

        // Checks if a user is eligible to act as an approver for a document
        bool CanBeApprover(User user, List<User> collaborators, User owner);

        // Verifies if a document can be submitted for approval by a specified approver
        bool CanSubmitForApproval(Document document, User approver);

        // Determines if a user has editing permissions based on their role and the document’s state
        bool CanEditDocument(User user, User owner, List<User> collaborators, string state);

        // Checks whether a user has access to view the document based on ownership, collaboration, or approver status
        bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state);
    }

}
