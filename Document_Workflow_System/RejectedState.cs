using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class RejectedState : IDocumentState
    {
        public void Edit(Document document, string content, User user)
        {
            if (document.Owner == user || document.Collaborators.Contains(user))
            {
                document.UpdateContent(content);
                document.UpdateLastEditedDate(DateTime.Now);
                document.State = new DraftState();
                Console.WriteLine("Document edited in Rejected state.");
            }
            else
            {
                throw new UnauthorizedAccessException("Only the owner or collaborators can edit the document in Rejected state.");
            }
        }

        public void Submit(Document document, User approver)
        {
            throw new InvalidOperationException("Error: Document must be edited before resubmitting for approval.");
        }

        public void Approve(Document document, User approver)
        {
            throw new InvalidOperationException("Cannot approve a document in Rejected state.");
        }

        public void Reject(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Document is already rejected.");
        }
        public void PushBack(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Pushback is not applicable for a rejected document.");
        }

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
