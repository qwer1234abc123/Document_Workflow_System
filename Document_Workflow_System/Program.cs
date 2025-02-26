﻿using Document_Workflow_System;

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
            var john = new User("John");
            var mary = new User("Mary");
            var steve = new User("Steve");

            users.Add(john);
            users.Add(mary);
            users.Add(steve);

            // Create documents
            var techReportFactory = new TechnicalReportFactory();
            var grantProposalFactory = new GrantProposalFactory();

            var techReport = new Document(techReportFactory, john);

            documentCollection.AddDocument(techReport);

            var grantProposal1 = new Document(grantProposalFactory, mary);

            documentCollection.AddDocument(grantProposal1);

            var grantProposal2 = new Document(grantProposalFactory, john);

            documentCollection.AddDocument(grantProposal2);

            // Display initialized documents
            Console.WriteLine("\nInitialized Documents:");
            var iterator = documentCollection.CreateIterator();

            while (iterator.HasNext())
            {
                var doc = iterator.Next();
                Console.WriteLine($"- {doc.Header.GetHeader()} (Owner: {doc.Owner.Username})"); 
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

            // Validate user input
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Please try again.");
                return;
            }

            // Ensure username uniqueness
            if (users.Exists(u => u.Username.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("User already exists. Please choose a different name.");
                return;
            }

            // Create new user
            users.Add(new User(name));
            Console.WriteLine($"User '{name}' created successfully.");
        }

        private static void LoginMenu()
        {
            Console.Write("Enter your username to login: ");
            string username = Console.ReadLine();

            // Find the user
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

                // Retrieve documents where the user is either an owner or a collaborator
                var iterator = documentCollection.CreateIterator(doc =>
                    doc.Owner == user || doc.Collaborators.Contains(user));

                var userDocs = new List<string>();
                while (iterator.HasNext())
                {
                    var doc = iterator.Next();
                    userDocs.Add(doc.Header.GetHeader());
                }

                if (userDocs.Count > 0)
                {
                    // Display user's associated documents
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
                Console.WriteLine($"- {doc.Header.GetHeader()} (Owner: {doc.Owner.Username})");
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
                    Console.WriteLine($"- {doc.Header.GetHeader()} (Owner)");
                }
                else if (doc.Collaborators.Contains(user))
                {
                    Console.WriteLine($"- {doc.Header.GetHeader()} (Collaborator)");
                }
                else if (doc.Approver == user)
                {
                    Console.WriteLine($"- {doc.Header.GetHeader()} (Approver)");
                }
            }
        }
        private static void CreateNewDocument(User user)
        {
            Console.WriteLine("\n=== Create New Document ===");
            Console.Write("Enter document type (1 for Technical Report, 2 for Grant Proposal): ");
            string type = Console.ReadLine();

            // Assign the appropriate document factory
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

            // Ensure the document header is unique
            var iterator = documentCollection.CreateIterator();
            while (iterator.HasNext())
            {
                var existingDocument = iterator.Next();
                if (existingDocument.Header.GetHeader().Equals(header, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Error: A document with the header '{header}' already exists. Please choose a unique header.");
                    return;
                }
            }

            Console.Write("Enter document footer: ");
            string footer = Console.ReadLine();

            // Create and add the new document
            var document = new Document(factory, user);
            document.Header.SetHeader(header);
            document.Footer.SetFooter(footer);

            documentCollection.AddDocument(document);
            Console.WriteLine($"Document '{header}' created successfully.");
        }

        private static void EditExistingDocument(User user)
        {
            Console.Write("\nEnter the name of the document to edit: ");
            string documentName = Console.ReadLine();

            // Find the document by its header
            var iterator = documentCollection.CreateIterator(doc =>
                    doc.Header.GetHeader().Equals(documentName, StringComparison.OrdinalIgnoreCase));

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
                Console.WriteLine("You have no access to this document.");
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
                    // check if document has approver
                    if (document.Approver != null)
                    {
                        document.SubmitForApproval(document.Approver);
                        break;
                    }
                    // 
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
                        document.PushBack(reason, user);
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
            Console.WriteLine($"Header: {document.Header.GetHeader()}");  
            Console.WriteLine($"Content: {document.Content.GetContent()}");  
            Console.WriteLine($"Additional Component: {document.AdditionalComponent.GetAdditionalComponent()}");  // ✅ FIXED
            Console.WriteLine($"Footer: {document.Footer.GetFooter()}"); 
            Console.WriteLine($"State: {document.State.GetType().Name}");
            Console.WriteLine($"Owner: {document.Owner.Username}");
        }

    }
}