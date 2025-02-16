using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class ApprovedState : IDocumentState
    {
        // Editing is not allowed once a document is approved
        public void Edit(Document document, string content, User user)
        {
            throw new InvalidOperationException("Document cannot be edited after approval.");
        }

        // A document that is already approved cannot be submitted for approval again
        public void Submit(Document document, User approver)
        {
            throw new InvalidOperationException("Approved document cannot be resubmitted.");
        }

        // A document that has already been approved cannot be approved again
        public void Approve(Document document, User approver)
        {
            throw new InvalidOperationException("Document is already approved.");
        }

        // Once a document is approved, it cannot be rejected
        public void Reject(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Approved document cannot be rejected.");
        }

        // Pushback is not applicable to approved documents
        public void PushBack(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Approved documents cannot be pushed back.");
        }

        // Defines the actions allowed in the Approved state
        public List<string> GetValidActions(Document document, User user)
        {
            var actions = new List<string>
        {
            "Set File Conversion Type",  // Allows setting a conversion strategy (e.g., PDF, Word)
            "Produce Converted File",    // Allows generating a converted version of the document
            "Show Document Contents"     // Allows viewing the document content
        };

            return actions;
        }
    }

}
