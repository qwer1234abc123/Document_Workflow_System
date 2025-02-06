using Document_Workflow_System;

public class PushedBackState : IDocumentState
{
    public void Edit(Document document, string content, User user)
    {
        if (document.Owner == user || document.Collaborators.Contains(user))
        {
            document.UpdateContent(content);
            document.UpdateLastEditedDate(DateTime.Now);
            Console.WriteLine("Document edited in Pushed Back state.");
        }
        else
        {
            throw new UnauthorizedAccessException("Only the owner or collaborators can edit the document in Pushed Back state.");
        }
    }

    public void Submit(Document document, User approver)
    {
        if (document.Approver == null)
        {
            throw new InvalidOperationException("Approver must be set for this document.");
        }

        if (document.Approver != approver)
        {
            throw new InvalidOperationException($"Document must be resubmitted to the retained approver '{document.Approver.Username}'.");
        }

        document.State = new UnderReviewState();

        Console.WriteLine($"Document resubmitted for approval to {document.Approver.Username}. State changed to 'Under Review'.");
        document.NotifyObservers($"Document '{document.Header}' was resubmitted for approval by {document.Owner.Username} to {document.Approver.Username}.");
        document.Approver.Notify($"You have been reassigned as the approver for the document '{document.Header}'.");
    }


    public void Approve(Document document, User approver)
    {
        throw new InvalidOperationException("Cannot approve a document in Pushed Back state.");
    }

    public void Reject(Document document, string reason, User approver)
    {
        throw new InvalidOperationException("Cannot reject a document in Pushed Back state.");
    }

    public void PushBack(Document document, string reason, User approver)
    {
        throw new InvalidOperationException("Document is already pushed back.");
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
