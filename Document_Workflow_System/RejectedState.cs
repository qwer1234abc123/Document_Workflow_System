using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class RejectedState : IDocumentState
    {
        // Allows the document owner or collaborators to edit the document.
        // Once edited, the document transitions back to the DraftState.
        public void Edit(Document document, string content, User user)
        {
            if (document.Owner == user || document.Collaborators.Contains(user))
            {
                document.UpdateContent(content);
                document.UpdateLastEditedDate(DateTime.Now);

                // Transition the document back to DraftState after modification.
                document.State = new DraftState();
                Console.WriteLine("Document edited in Rejected state.");
            }
            else
            {
                throw new UnauthorizedAccessException("Only the owner or collaborators can edit the document in Rejected state.");
            }
        }

        // Prevents resubmission unless the document has been edited.
        public void Submit(Document document, User approver)
        {
            throw new InvalidOperationException("Error: Document must be edited before resubmitting for approval.");
        }

        // Prevents approving a document that has been previously rejected.
        public void Approve(Document document, User approver)
        {
            throw new InvalidOperationException("Cannot approve a document in Rejected state.");
        }

        // Prevents rejecting a document that is already in the Rejected state.
        public void Reject(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Document is already rejected.");
        }

        // Pushback is not applicable to a rejected document.
        public void PushBack(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Pushback is not applicable for a rejected document.");
        }

        // Returns the list of valid actions based on the document's rejected state.
        public List<string> GetValidActions(Document document, User user)
        {
            var actions = new List<string>();

            // Editing and resubmission are only allowed for the owner or collaborators.
            if (document.Owner == user || document.Collaborators.Contains(user))
            {
                actions.Add("Edit Document Content");
                actions.Add("Submit for Review");
            }

            // The owner can still add collaborators.
            if (document.Owner == user)
            {
                actions.Add("Add Collaborator");
            }

            // Common actions available to all users.
            actions.Add("Set File Conversion Type");
            actions.Add("Produce Converted File");
            actions.Add("Show Document Contents");

            return actions;
        }
    }

}
