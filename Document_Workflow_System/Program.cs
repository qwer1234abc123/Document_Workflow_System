using Document_Workflow_System;

namespace DocumentWorkflowSystem
{
    class Program
    {
        // Global collections for users and documents
        private static List<User> users = new List<User>();
        private static DocumentCollection documentCollection = new DocumentCollection();

        static void Main(string[] args)
        {
            // Initialize default users and documents
            InitializeSystem();

            // Start the main menu
            MainMenu();
        }

        private static void InitializeSystem()
        {
            Console.WriteLine("=== Initializing System ===");

            // Create users
            var john = new User("John", UserRole.Owner);
            var mary = new User("Mary", UserRole.Owner);
            var steve = new User("Steve", UserRole.Owner);
            users.Add(john);
            users.Add(mary);
            users.Add(steve);

            // Create documents
            var techReportFactory = new TechnicalReportFactory();
            var grantProposalFactory = new GrantProposalFactory();

            var techReport = new Document(techReportFactory, john)
            {
                Header = "Technical Report: AI Research",
                Footer = "Footer for AI Research Report"
            };
            documentCollection.AddDocument(techReport);

            var grantProposal1 = new Document(grantProposalFactory, mary)
            {
                Header = "Grant Proposal: Green Energy Project",
                Footer = "Footer for Green Energy Proposal"
            };
            documentCollection.AddDocument(grantProposal1);

            var grantProposal2 = new Document(grantProposalFactory, john)
            {
                Header = "Grant Proposal: Space Exploration Initiative",
                Footer = "Footer for Space Exploration Proposal"
            };
            documentCollection.AddDocument(grantProposal2);

            // Print initialized documents
            Console.WriteLine("\nInitialized Documents:");
            var iterator = documentCollection.CreateIterator();

            while (iterator.HasNext())
            {
                var doc = iterator.Next();
                Console.WriteLine($"- {doc.Header} (Owner: {doc.Owner.Username})");
            }
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Login User");
                Console.WriteLine("3. List Users");
                Console.WriteLine("4. List Documents");
                Console.WriteLine("5. Exit Program");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        LoginMenu();
                        break;
                    case "3":
                        ListUsers();
                        break;
                    case "4":
                        ListDocuments();
                        break;
                    case "5":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private static void CreateUser()
        {
            Console.Write("Enter new user's name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Please try again.");
                return;
            }

            if (users.Exists(u => u.Username.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("User already exists. Please choose a different name.");
                return;
            }

            users.Add(new User(name, UserRole.Owner));
            Console.WriteLine($"User '{name}' created successfully.");
        }

        private static void LoginMenu()
        {
            Console.Write("Enter your username to login: ");
            string username = Console.ReadLine();

            var user = users.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                Console.WriteLine("User not found. Please try again.");
                return;
            }

            Console.WriteLine($"Welcome, {user.Username}!");
            UserMenu(user);
        }

        private static void ListUsers()
        {
            Console.WriteLine("\n=== List of Users ===");
            foreach (var user in users)
            {
                Console.Write($"- {user.Username} ");
                var iterator = documentCollection.CreateIterator(doc =>
                    doc.Owner == user || doc.Collaborators.Contains(user));

                var userDocs = new List<string>();
                while (iterator.HasNext())
                {
                    var doc = iterator.Next();
                    userDocs.Add(doc.Header);
                }

                if (userDocs.Count > 0)
                {
                    Console.Write("(" + string.Join(", ", userDocs) + ")");
                }
                else
                {
                    Console.Write("(No documents)");
                }
                Console.WriteLine();
            }
        }

        private static void ListDocuments()
        {
            Console.WriteLine("\n=== List of Documents ===");
            var iterator = documentCollection.CreateIterator();

            while (iterator.HasNext())
            {
                var doc = iterator.Next();
                Console.WriteLine($"- {doc.Header} (Owner: {doc.Owner.Username})");
            }
        }

        private static void UserMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("\n=== User Menu ===");
                Console.WriteLine("1. Create New Document");
                Console.WriteLine("2. Edit Existing Document");
                Console.WriteLine("3. List Your Documents");
                Console.WriteLine("0. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewDocument(user);
                        break;
                    case "2":
                        EditExistingDocument(user);
                        break;
                    case "3":
                        ListUserDocuments(user);
                        break;
                    case "0":
                        Console.WriteLine($"Goodbye, {user.Username}!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void ListUserDocuments(User user)
        {
            Console.WriteLine($"\n=== Documents for {user.Username} ===");
            // Filter by ownership, collaboration, or approver role
            var iterator = documentCollection.CreateIterator(doc =>
                doc.Owner == user || doc.Collaborators.Contains(user) || doc.Approver == user);

            while (iterator.HasNext())
            {
                var doc = iterator.Next();
                if (doc.Owner == user)
                {
                    Console.WriteLine($"- {doc.Header} (Owner)");
                }
                else if (doc.Collaborators.Contains(user))
                {
                    Console.WriteLine($"- {doc.Header} (Collaborator)");
                }
                else if (doc.Approver == user)
                {
                    Console.WriteLine($"- {doc.Header} (Approver)");
                }
            }
        }
        private static void CreateNewDocument(User user)
        {
            Console.WriteLine("\n=== Create New Document ===");
            Console.Write("Enter document type (1 for Technical Report, 2 for Grant Proposal): ");
            string type = Console.ReadLine();

            IDocumentFactory factory = type switch
            {
                "1" => new TechnicalReportFactory(),
                "2" => new GrantProposalFactory(),
                _ => null
            };

            if (factory == null)
            {
                Console.WriteLine("Invalid document type. Please try again.");
                return;
            }

            Console.Write("Enter document header: ");
            string header = Console.ReadLine();

            Console.Write("Enter document footer: ");
            string footer = Console.ReadLine();

            var document = new Document(factory, user)
            {
                Header = header,
                Footer = footer
            };

            documentCollection.AddDocument(document);
            Console.WriteLine($"Document '{header}' created successfully.");
        }
        private static void EditExistingDocument(User user)
        {
            Console.Write("\nEnter the name of the document to edit: ");
            string documentName = Console.ReadLine();

            var iterator = documentCollection.CreateIterator(doc =>
                    doc.Header.Equals(documentName, StringComparison.OrdinalIgnoreCase));

            Document document = null;
            if (iterator.HasNext())
            {
                document = iterator.Next();
            }

            if (document == null)
            {
                Console.WriteLine("Document not found. Please try again.");
                return;
            }

            DocumentEditMenu(user, document);
        }

        private static void DocumentEditMenu(User user, Document document)
        {
            if (!document.CanAccess(user))
            {
                Console.WriteLine("You no longer have access to this document.");
                return;
            }
            while (true)
            {
                Console.WriteLine("\n=== Edit Document Menu ===");

                // Get valid actions from the current state
                var actions = document.State.GetValidActions(document, user);

                // Display menu dynamically
                int optionNumber = 1;
                foreach (var action in actions)
                {
                    Console.WriteLine($"{optionNumber++}. {action}");
                }
                Console.WriteLine("0. Return to User Menu");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "0") return;

                if (int.TryParse(choice, out int index) && index > 0 && index <= actions.Count)
                {
                    var action = actions[index - 1];
                    ExecuteAction(action, user, document); // Dynamically execute the selected action
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        private static void ExecuteAction(string action, User user, Document document)
        {
            switch (action)
            {
                case "Edit Document Content":
                    Console.Write("Enter new document content: ");
                    string newContent = Console.ReadLine();
                    document.Edit(newContent, user);
                    break;

                case "Submit for Review":
                    Console.Write("Enter approver's username: ");
                    string approverName = Console.ReadLine();
                    var approver = users.Find(u => u.Username.Equals(approverName, StringComparison.OrdinalIgnoreCase));
                    if (approver != null)
                    {
                        document.SubmitForApproval(approver);
                    }
                    else
                    {
                        Console.WriteLine("Invalid approver.");
                    }
                    break;

                case "Push Back":
                    if (document.Approver == user)
                    {
                        Console.Write("Enter reason for pushing back: ");
                        string reason = Console.ReadLine();
                        document.State.PushBack(document, reason, user);
                    }
                    else
                    {
                        Console.WriteLine("Error: Only the approver can push back the document.");
                    }
                    break;

                case "Approve Document":
                    if (document.Approver == user)
                    {
                        document.Approve(user);
                    }
                    else
                    {
                        Console.WriteLine("Error: Only the approver can approve the document.");
                    }
                    break;

                case "Reject Document":
                    if (document.Approver == user)
                    {
                        Console.Write("Enter reason for rejection: ");
                        string rejectReason = Console.ReadLine();
                        document.Reject(rejectReason, user);
                    }
                    else
                    {
                        Console.WriteLine("Error: Only the approver can reject the document.");
                    }
                    break;

                case "Add Collaborator":
                    if (document.Owner == user)
                    {
                        Console.Write("Enter collaborator's username: ");
                        string collaboratorName = Console.ReadLine();
                        var collaborator = users.Find(u => u.Username.Equals(collaboratorName, StringComparison.OrdinalIgnoreCase));
                        if (collaborator != null)
                        {
                            document.AddCollaborator(collaborator);
                        }
                        else
                        {
                            Console.WriteLine("Invalid collaborator.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Only the owner can add collaborators.");
                    }
                    break;

                case "Set File Conversion Type":
                    SetConversionType(document);
                    break;

                case "Produce Converted File":
                    ProduceConvertedFile(document);
                    break;

                case "Show Document Contents":
                    ShowDocumentContents(document);
                    break;

                default:
                    Console.WriteLine("Invalid action.");
                    break;
            }
        }


        private static void EditDocumentContent(User user, Document document)
        {
            if (document.State.GetType().Name == "UnderReviewState")
            {
                Console.WriteLine("Document cannot be edited while under review.");
                return;
            }

            if (document.Owner != user && !document.Collaborators.Contains(user))
            {
                Console.WriteLine("You do not have permission to edit this document.");
                return;
            }

            Console.Write("Enter new document content: ");
            string newContent = Console.ReadLine();
            document.Edit(newContent, user);
        }
        private static void SubmitForReview(User user, Document document)
        {
            Console.WriteLine("\nAvailable approvers:");
            foreach (var approverCandidate in users)
            {
                if (document.CanBeApprover(approverCandidate))
                {
                    Console.WriteLine($"- {approverCandidate.Username}");
                }
            }

            Console.Write("\nEnter the name of the approver: ");
            string approverName = Console.ReadLine();

            var approver = users.Find(u => u.Username.Equals(approverName, StringComparison.OrdinalIgnoreCase));
            if (approver == null)
            {
                Console.WriteLine("Error: Invalid approver.");
                return;
            }

            // Delegate validation and submission logic to the Document class
            document.SubmitForApproval(approver);
        }

        private static void PushBackDocument(User user, Document document)
        {
            if (document.Approver != user)
            {
                Console.WriteLine("Only the assigned approver can push back the document.");
                return;
            }

            Console.Write("Enter reason for pushing back: ");
            string reason = Console.ReadLine();
            document.State = new DraftState(); // Push back changes state to DraftState
            Console.WriteLine($"Document pushed back with reason: {reason}");
        }
        private static void ApproveDocument(User user, Document document)
        {
            if (document.Approver != user)
            {
                Console.WriteLine("Only the assigned approver can approve the document.");
                return;
            }

            document.Approve(user);
        }
        private static void RejectDocument(User user, Document document)
        {
            if (document.Approver != user)
            {
                Console.WriteLine("Only the assigned approver can reject the document.");
                return;
            }

            Console.Write("Enter reason for rejection: ");
            string reason = Console.ReadLine();
            document.Reject(reason, user);
        }
        private static void AddCollaborator(User user, Document document)
        {
            Console.WriteLine("\nAvailable collaborators:");
            foreach (var potentialCollaborator in users)
            {
                if (document.CanAddCollaborator(potentialCollaborator))
                {
                    Console.WriteLine($"- {potentialCollaborator.Username}");
                }
            }

            Console.Write("\nEnter collaborator's username: ");
            string collaboratorName = Console.ReadLine();

            var collaborator = users.Find(u => u.Username.Equals(collaboratorName, StringComparison.OrdinalIgnoreCase));
            if (collaborator == null)
            {
                Console.WriteLine($"Error: User '{collaboratorName}' does not exist.");
                return;
            }

            // Delegate adding the collaborator to the Document class
            document.AddCollaborator(collaborator);
        }

        private static void SetConversionType(Document document)
        {
            Console.WriteLine("Choose conversion type:");
            Console.WriteLine("1. Word");
            Console.WriteLine("2. PDF");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    document.SetConversionStrategy(new WordConversionStrategy());
                    Console.WriteLine("Conversion type set to Word.");
                    break;
                case "2":
                    document.SetConversionStrategy(new PDFConversionStrategy());
                    Console.WriteLine("Conversion type set to PDF.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        private static void ProduceConvertedFile(Document document)
        {
            try
            {
                string convertedFile = document.Convert();
                Console.WriteLine("Converted File:");
                Console.WriteLine(convertedFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private static void ShowDocumentContents(Document document)
        {
            Console.WriteLine("\nDocument Contents:");
            Console.WriteLine($"Header: {document.Header}");
            Console.WriteLine($"Content: {document.Content}");
            Console.WriteLine($"Footer: {document.Footer}");
            Console.WriteLine($"State: {document.State.GetType().Name}");
            Console.WriteLine($"Owner: {document.Owner.Username}");
        }
    }
}