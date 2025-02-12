using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class UnderReviewState : IDocumentState
    {
        public void Edit(Document document, string content, User user)
        {
            throw new InvalidOperationException("Document cannot be edited while under review.");
        }

        public void Submit(Document document, User approver)
        {
            throw new InvalidOperationException("Document is already under review.");
        }

        public void Approve(Document document, User approver)
        {
            if (document.Approver != approver)
            {
                throw new UnauthorizedAccessException("Only the assigned approver can approve this document.");
            }

            document.State = new ApprovedState();
            Console.WriteLine("Document approved. State changed to 'Approved'.");
        }

        public void Reject(Document document, string reason, User approver)
        {
            if (document.Approver != approver)
            {
                throw new UnauthorizedAccessException("Only the assigned approver can reject this document.");
            }

            document.State = new RejectedState();
            Console.WriteLine($"Document rejected with reason: {reason}. State changed to 'Rejected'.");
        }
        public void PushBack(Document document, string reason, User approver)
        {
            if (document.Approver != approver)
            {
                throw new UnauthorizedAccessException("Only the assigned approver can push back this document.");
            }

            // Transition to DraftState
            document.State = new DraftState();

            // Retain the current approver
            Console.WriteLine($"Document pushed back with reason: {reason}. State changed to 'Draft'.");
            document.NotifyObservers($"Document '{document.Header}' was pushed back by {approver.Username} with reason: {reason}.");
        }



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

            // Allow adding collaborators for the owner
            if (document.Owner == user)
            {
                actions.Add("Add Collaborator");
            }

            // Common actions for all roles
            actions.Add("Set File Conversion Type");
            actions.Add("Produce Converted File");
            actions.Add("Show Document Contents");

            return actions;
        }


    }
}
