using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Workflow_System
{
    public class Document : INotificationSubject
    {
        // Document properties
        public IHeader Header { get; private set; }  // Stores document header
        public IFooter Footer { get; private set; }  // Stores document footer
        public IContent Content { get; private set; } // Stores document content
        public IAdditionalComponent AdditionalComponent { get; private set; } // Additional document component
        public IDocumentState State { get; set; }  // Holds the current state of the document
        public User Owner { get; private set; }  // Owner of the document
        public List<User> Collaborators { get; private set; }  // List of document collaborators
        public User Approver { get; private set; }  // The user assigned as the document approver
        public DateTime CreatedDate { get; private set; }  // Date when the document was created
        public DateTime? LastEditedDate { get; set; }  // Date when the document was last edited

        private List<INotifiable> observers;  // List of observers for notifications
        private IConversionStrategy conversionStrategy;  // Strategy for converting the document format
        private readonly IAccessControlSubject accessControlProxy;  // Proxy for access control validation

        // Constructor initializes document properties
        public Document(IDocumentFactory factory, User owner)
        {
            // Create document components using the provided factory
            Header = factory.CreateHeader();
            Footer = factory.CreateFooter();
            Content = factory.CreateContent();
            AdditionalComponent = factory.CreateAdditionalComponent();

            Owner = owner;
            Collaborators = new List<User>();
            observers = new List<INotifiable>();
            State = new DraftState(); // Set initial state as Draft
            CreatedDate = DateTime.Now;

            accessControlProxy = new AccessControlProxy(); // Initialize access control proxy
            RegisterObserver(owner); // Register the owner as an observer
        }

        // Checks if a user can be assigned as an approver
        public bool CanBeApprover(User user)
        {
            return accessControlProxy.CanBeApprover(user, Collaborators, Owner);
        }

        // Adds a collaborator to the document
        public void AddCollaborator(User user)
        {
            if (!accessControlProxy.CanAddCollaborator(user, Collaborators, Owner, Approver, State.GetType().Name))
            {
                Console.WriteLine($"Error: User '{user.Username}' cannot be added as a collaborator.");
                return;
            }

            Collaborators.Add(user);
            RegisterObserver(user); // Add the user as an observer for notifications
            NotifyObservers($"User {user.Username} added as a collaborator to '{Header.GetHeader()}'.");
        }

        // Checks if a user has access to the document
        public bool CanAccess(User user)
        {
            return accessControlProxy.CanAccessDocument(user, Owner, Collaborators, Approver, State.GetType().Name);
        }

        // Edits the document content
        public void Edit(string content, User user)
        {
            if (!accessControlProxy.CanEditDocument(user, Owner, Collaborators, State.GetType().Name))
            {
                throw new UnauthorizedAccessException("User is not authorized to edit this document.");
            }

            State.Edit(this, content, user);
            LastEditedDate = DateTime.Now;
            NotifyObservers($"Document '{Header.GetHeader()}' was edited by {user.Username}.");
        }

        // Submits the document for approval
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

        // Approves the document
        public void Approve(User approver)
        {
            State.Approve(this, approver);
            NotifyObservers($"Document '{Header.GetHeader()}' was approved by {approver.Username}.");
        }

        // Rejects the document with a reason
        public void Reject(string reason, User approver)
        {
            State.Reject(this, reason, approver);

            // Remove the approver as an observer if the document is rejected
            if (Approver == approver)
            {
                RemoveObserver(approver);
                Approver = null;
                Console.WriteLine($"Approver '{approver.Username}' has been removed from the document.");
            }
        }

        // Pushes the document back to a previous state with a reason
        public void PushBack(string reason, User approver)
        {
            State.PushBack(this, reason, approver);
        }

        // Updates the document content
        public void UpdateContent(string content)
        {
            Content.SetContent(content);
        }

        // Updates the last edited date
        public void UpdateLastEditedDate(DateTime date)
        {
            LastEditedDate = date;
        }

        // Sets an approver for the document
        public void SetApprover(User approver)
        {
            Approver = approver;
            RegisterObserver(approver);
        }

        // Sets the conversion strategy for the document
        public void SetConversionStrategy(IConversionStrategy strategy)
        {
            conversionStrategy = strategy;
        }

        // Converts the document using the assigned strategy
        public string Convert()
        {
            if (conversionStrategy == null)
                throw new InvalidOperationException("Conversion strategy is not set.");

            return conversionStrategy.Convert(this);
        }

        // Observer Pattern Methods
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

        // Returns a string representation of the document
        public override string ToString()
        {
            return $"Document: {Header.GetHeader()} | State: {State.GetType().Name} | Owner: {Owner.Username} | Created: {CreatedDate}";
        }
    }
}