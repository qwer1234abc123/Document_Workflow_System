using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IAccessControl
    {
        bool CanAddCollaborator(User user, List<User> collaborators, User owner, User approver, string state);
        bool CanBeApprover(User user, List<User> collaborators, User owner);
        bool CanSubmitForApproval(Document document, User approver);
        bool CanEditDocument(User user, User owner, List<User> collaborators, string state);
        bool CanAccessDocument(User user, User owner, List<User> collaborators, User approver, string state);
    }
}
