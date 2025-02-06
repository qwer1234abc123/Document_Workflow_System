using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class ApprovedState : IDocumentState
    {
        public void Edit(Document document, string content, User user)
        {
            throw new InvalidOperationException("Document cannot be edited after approval.");
        }

        public void Submit(Document document, User approver)
        {
            throw new InvalidOperationException("Approved document cannot be resubmitted.");
        }

        public void Approve(Document document, User approver)
        {
            throw new InvalidOperationException("Document is already approved.");
        }

        public void Reject(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Approved document cannot be rejected.");
        }
        public void PushBack(Document document, string reason, User approver)
        {
            throw new InvalidOperationException("Approved documents cannot be pushed back.");
        }

        public List<string> GetValidActions(Document document, User user)
        {
            var actions = new List<string>
    {
        "Set File Conversion Type",
        "Produce Converted File",
        "Show Document Contents"
    };

            return actions;
        }

    }
}
