using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public interface IDocumentState
    {
        void Edit(Document document, string content, User user);
        void Submit(Document document, User approver);
        void Approve(Document document, User approver);
        void Reject(Document document, string reason, User approver);
        void PushBack(Document document, string reason, User approver);
        List<string> GetValidActions(Document document, User user);
    }


}
