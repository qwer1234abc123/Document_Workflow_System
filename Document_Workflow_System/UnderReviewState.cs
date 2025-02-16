using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class UnderReviewState : IDocumentState
    {
        // Documents cannot be edited while under review to maintain integrity.
        public void Edit(Document document, string content, User user)
        {
            throw new InvalidOperationException("Document cannot be edited while under review.");
        }

        // Prevents resubmission since the document is already under review.
        public void Submit(Document document, User approver)
        {
            throw new InvalidOperationException("Document is already under review.");
        }

        // Approves the document if the user is the assigned approver.
        public void Approve(Document document, User approver)
        {
            if (document.Approver != approver)
            {
                throw new UnauthorizedAccessException("Only the assigned approver can approve this document.");
            }

            document.State = new ApprovedState(); // Transition to ApprovedState
            Console.WriteLine("Document approved. State changed to 'Approved'.");
        }

        // Rejects the document if the user is the assigned approver.
        public void Reject(Document document, string reason, User approver)
        {
            if (document.Approver != approver)
            {
                throw new UnauthorizedAccessException("Only the assigned approver can reject this document.");
            }

            document.State = new RejectedState(); // Transition to RejectedState
            document.LastEditedDate = null; // Reset last edited date to enforce modifications before resubmission

            Console.WriteLine($"Document rejected with reason: {reason}. State changed to 'Rejected'.");
            document.NotifyObservers($"Document '{document.Header.GetHeader()}' was rejected by {approver.Username} with reason: {reason}.");
        }

        // Pushes the document back to the DraftState for further modifications.
        public void PushBack(Document document, string reason, User approver)
        {
            if (document.Approver != approver)
            {
                throw new UnauthorizedAccessException("Only the assigned approver can push back this document.");
            }

            document.State = new DraftState(); // Transition to DraftState

            Console.WriteLine($"Document pushed back with reason: {reason}. State changed to 'Draft'.");
            document.NotifyObservers($"Document '{document.Header.GetHeader()}' was pushed back by {approver.Username} with reason: {reason}.");
        }

        // Retrieves the list of valid actions based on the document state and user's role.
        public List<string> GetValidActions(Document document, User user)
        {
            var actions = new List<string>();

            // Approver-specific actions
            if (document.Approver == user)
            {
                actions.Add("Push Back");
                actions.Add("Approve Document");
                actions.Add("Reject Document");
            }

            // Owner can add collaborators even during review
            if (document.Owner == user)
            {
                actions.Add("Add Collaborator");
            }

            // Common actions available for all users
            actions.Add("Set File Conversion Type");
            actions.Add("Produce Converted File");
            actions.Add("Show Document Contents");

            return actions;
        }
    }

}
