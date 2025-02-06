using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class Document : INotificationSubject
    {
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Content { get; private set; }
        public IDocumentState State { get; set; }
        public User Owner { get; private set; }
        public List<User> Collaborators { get; private set; }
        public User Approver { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? LastEditedDate { get; private set; }

        private List<INotifiable> observers;
        private IConversionStrategy conversionStrategy;

        public Document(IDocumentFactory factory, User owner)
        {
            Header = factory.CreateHeader();
            Footer = factory.CreateFooter();
            Content = factory.CreateContent();
            Owner = owner;
            Collaborators = new List<User>();
            observers = new List<INotifiable>();
            State = new DraftState();
            CreatedDate = DateTime.Now;

            RegisterObserver(owner);
        }


        public void Edit(string content, User user)
        {
            State.Edit(this, content, user);
            NotifyObservers($"Document '{Header}' was edited by {user.Username}.");
        }

        public void SubmitForApproval(User approver)
        {
            State.Submit(this, approver);
        }

        public void Approve(User approver)
        {
            State.Approve(this, approver);
            NotifyObservers($"Document '{Header}' was approved by {approver.Username}.");
            approver.Notify($"You approved the document '{Header}'.");
        }

        public void Reject(string reason, User approver)
        {
            State.Reject(this, reason, approver);

            // Remove the approver as an observer after rejection
            if (Approver == approver)
            {
                RemoveObserver(approver);
                Approver = null; // Clear the approver field
                Console.WriteLine($"Approver '{approver.Username}' has been removed from the document.");
            }

            NotifyObservers($"Document '{Header}' was rejected by {approver.Username} with reason: {reason}.");
        }

        public void AddCollaborator(User collaborator)
        {
            Collaborators.Add(collaborator);
            RegisterObserver(collaborator);
            NotifyObservers($"User {collaborator.Username} has been added as a collaborator to the document '{Header}'.");
        }

        public void UpdateContent(string content)
        {
            Content = content;
        }

        public void UpdateLastEditedDate(DateTime date)
        {
            LastEditedDate = date;
        }

        public void SetApprover(User approver)
        {
            Approver = approver;
            RegisterObserver(approver);
        }

        // ✅ Conversion strategy logic remains unchanged
        public void SetConversionStrategy(IConversionStrategy strategy)
        {
            conversionStrategy = strategy;
        }

        public string Convert()
        {
            if (conversionStrategy == null)
                throw new InvalidOperationException("Conversion strategy is not set.");

            return conversionStrategy.Convert(this);
        }

        // ✅ Observer Pattern Methods (No changes needed)
        public void RegisterObserver(INotifiable observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(INotifiable observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in observers)
            {
                observer.Notify(message);
            }
        }

        public override string ToString()
        {
            return $"Document: {Header} | State: {State.GetType().Name} | Owner: {Owner.Username} | Created: {CreatedDate}";
        }
    }
}
