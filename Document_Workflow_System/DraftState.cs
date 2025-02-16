using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class DraftState : IDocumentState
    {
        // Allows editing of the document if the user is the owner or a collaborator
        public void Edit(Document document, string content, User user)
        {
            if (document.Owner == user || document.Collaborators.Contains(user))
            {
                document.UpdateContent(content);
                document.UpdateLastEditedDate(DateTime.Now);
                Console.WriteLine("Document edited in Draft state.");
            }
            else
            {
                throw new UnauthorizedAccessException("Only the owner or collaborators can edit the document in Draft state.");
            }
        }

        // Submits the document for review, setting the approver and changing the state
        public void Submit(Document document, User approver)
        {
            if (!document.CanBeApprover(approver))
            {
                throw new InvalidOperationException($"User '{approver.Username}' cannot be an approver for this document.");
            }

            if (document.Approver != null) // Check if the document already has an approver
            {
                Console.WriteLine($"Document submitted for approval to {approver.Username}. State changed to 'Under Review'.");
                document.NotifyObservers($"Document '{document.Header.GetHeader()}' was submitted for approval by {document.Owner.Username} to {approver.Username}.");
            }
            else
            {
                document.SetApprover(approver); // Assign approver if none exists
                Console.WriteLine($"Document submitted for approval to {approver.Username}. State changed to 'Under Review'.");
                document.NotifyObservers($"Document '{document.Header.GetHeader()}' was submitted for approval by {document.Owner.Username} to {approver.Username}.");
            }

            // Transition to UnderReviewState
            document.State = new UnderReviewState();
        }

        // Prevents approval of a document while in the Draft state
        public void Approve(Document document, User approver)
        {
            throw new InvalidOperationException("Cannot approve a document in Draft state.");
        }

        // Prevents rejection of a document while in the Draft state
        public void Reject(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Cannot reject a document in Draft state.");
        }

        // Prevents pushback while in the Draft state
        public void PushBack(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Pushback is not applicable in Draft state.");
        }

        // Returns a list of valid actions available in the Draft state based on user role
        public List<string> GetValidActions(Document document, User user)
        {
            var actions = new List<string>();

            if (document.Owner == user || document.Collaborators.Contains(user))
            {
                actions.Add("Edit Document Content");
                actions.Add("Submit for Review");
            }
            if (document.Owner == user)
            {
                actions.Add("Add Collaborator");
            }

            actions.Add("Set File Conversion Type");
            actions.Add("Produce Converted File");
            actions.Add("Show Document Contents");

            return actions;
        }
    }

}
