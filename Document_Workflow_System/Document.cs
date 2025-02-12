using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class Document : INotificationSubject
    {
        public IHeader Header { get; private set; }
        public IFooter Footer { get; private set; } 
        public IContent Content { get; private set; }
        public IAdditionalComponent AdditionalComponent { get; private set; }
        public IDocumentState State { get; set; }
        public User Owner { get; private set; }
        public List<User> Collaborators { get; private set; }
        public User Approver { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? LastEditedDate { get; private set; }
        private List<INotifiable> observers;
        private IConversionStrategy conversionStrategy;
        private readonly AccessControlFacade accessControlFacade;

        public Document(IDocumentFactory factory, User owner)
        {
            Header = factory.CreateHeader();
            Footer = factory.CreateFooter();
            Content = factory.CreateContent();
            AdditionalComponent = factory.CreateAdditionalComponent();
            Owner = owner;
            Collaborators = new List<User>();
            observers = new List<INotifiable>();
            State = new DraftState();
            CreatedDate = DateTime.Now;

            accessControlFacade = new AccessControlFacade(); // Initialize facade
            RegisterObserver(owner);
        }

        public bool CanBeApprover(User user)
        {
            return accessControlFacade.CanBeApprover(user, Collaborators, Owner);

        }

        public void AddCollaborator(User user)
        {
            if (!accessControlFacade.CanAddCollaborator(user, Collaborators, Owner, Approver))
            {
                Console.WriteLine($"Error: User '{user.Username}' cannot be added as a collaborator to this document.");
                return; // Gracefully exit
            }

            Collaborators.Add(user);
            RegisterObserver(user);
            NotifyObservers($"User {user.Username} has been added as a collaborator to the document '{Header.GetHeader()}'.");
            Console.WriteLine($"User '{user.Username}' added as a collaborator.");
        }


        public bool CanAddCollaborator(User user)
        {
            return accessControlFacade.CanAddCollaborator(user, Collaborators, Owner, Approver);
        }

        public bool CanAccess(User user)
        {
            return accessControlFacade.CanAccessDocument(user, Owner, Collaborators, Approver, State.GetType().Name);
        }



        public void Edit(string content, User user)
        {
            if (!accessControlFacade.CanEditDocument(user, Owner, Collaborators, State.GetType().Name))
            {
                throw new UnauthorizedAccessException("User is not authorized to edit this document.");
            }

            State.Edit(this, content, user);
            NotifyObservers($"Document '{Header.GetHeader()}' was edited by {user.Username}.");
        }

        public void SubmitForApproval(User approver = null)
        {
            try
            {
                State.Submit(this, approver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Approve(User approver)
        {
            State.Approve(this, approver);
            NotifyObservers($"Document '{Header.GetHeader()}' was approved by {approver.Username}.");
            approver.Notify($"You approved the document '{Header.GetHeader()}'.");
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

            NotifyObservers($"Document '{Header.GetHeader()}' was rejected by {approver.Username} with reason: {reason}.");
        }


        public void UpdateContent(string content)
        {
            Content.SetContent(content);
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

        // New: Set Conversion Strategy
        public void SetConversionStrategy(IConversionStrategy strategy)
        {
            conversionStrategy = strategy;
        }

        // New: Convert Document
        public string Convert()
        {
            if (conversionStrategy == null)
                throw new InvalidOperationException("Conversion strategy is not set.");

            return conversionStrategy.Convert(this);
        }

        // Observer methods
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
            return $"Document: {Header.GetHeader()} | State: {State.GetType().Name} | Owner: {Owner.Username} | Created: {CreatedDate}";
        }

    }
}