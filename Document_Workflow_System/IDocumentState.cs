using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IDocumentState
    {
        // Allows editing the document if the current state permits it.
        void Edit(Document document, string content, User user);

        // Submits the document for approval, transitioning to the next state.
        void Submit(Document document, User approver);

        // Approves the document if the current state allows it.
        void Approve(Document document, User approver);

        // Rejects the document and may transition it to a different state.
        void Reject(Document document, string reason, User approver);

        // Pushes back the document for revisions instead of outright rejecting it.
        void PushBack(Document document, string reason, User approver);

        // Returns a list of valid actions a user can perform based on the current state.
        List<string> GetValidActions(Document document, User user);
    }


}
