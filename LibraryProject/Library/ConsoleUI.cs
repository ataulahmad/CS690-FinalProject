namespace Library;

using System.ComponentModel.Design;
using Spectre.Console;

public class ConsoleUI {
    
    LibraryInventory library;

    public ConsoleUI() {
        library = LibraryInventory.getInstance();
    }

    public void LoginMenu() {
        var username = AnsiConsole.Prompt(new TextPrompt<string>("Enter username:"));
        var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

        Boolean authenticated = library.Login(username, password);

        if (authenticated) {
            MainMenu();
        } else {
            Console.WriteLine("The combination of your user/password is incorrect!");
        }
    }

    private void MainMenu() {
        var command = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What's your [green]choice[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] {
                    "Add Book", "Show Book", "Quit",
                }
            )
        );

        AnsiConsole.WriteLine($"{command}");

        if (command == "Add Book") {
            AddBookMenu();
        } else if (command == "Show Book") {
            ShowBookMenu();
        } else if (command == "Quit") {
            Console.WriteLine("Quited");
        }
        
    }

    private void AddBookMenu() {
        Console.WriteLine("=== Add Book ===");

        string title = AskForInput("Enter title: ");
        string genre = AskForInput("Enter genre: ");
        string isbn = AskForInput("Enter isbn: ");
        string description = AskForInput("Enter description: ");

        Book book = new Book(title, genre, isbn, description, null, null);

        library.AddBook(book);

        var confirmation = booleanPrompt("Add new Book?");
            
        if (confirmation) {
            AddBookMenu();
        } else {
            MainMenu();
        }
    }

    private void ShowBookMenu() {
        Console.WriteLine("=== Show Book ===");

        string? searchTitle = AskForInput("Enter books title: ");

        Book? book = library.GetBook(searchTitle);

        if (book != null) {
            book.printBookDetails();
            if (book.customer == null) {
                CheckOutMenu(book);
            } else {
                CheckInMenu(book);
            }
        } else {
            Console.WriteLine("No Book found!");
        }

        var confirmation = booleanPrompt("Search another Book?");
            
        if (confirmation) {
            ShowBookMenu();
        } else {
            MainMenu();
        }
    }

    private string AddCustomerMenu() {
        Console.WriteLine("=== Add Customer ===");

        string name = AskForInput("Enter Name: ");
        string address = AskForInput("Enter Address: ");
        string email = AskForInput("Enter Email: ");
        
        Customer customer = new Customer(name, address, email);

        var confirmation = booleanPrompt("Save Customer?");
            
        if (confirmation) {
            library.AddCustomer(customer);
            return name;
        }

        return null;
    }

    private void CheckOutMenu(Book book) {
        var checkout = booleanPrompt("Book is available. Checkout?");

        if (checkout) {
            string name = AskForInput("Enter Customer Name: ");
            Customer? customer = library.GetCustomer(name);
            if (customer == null) {
                name = AddCustomerMenu();
                customer = library.GetCustomer(name);
            }
            
            library.CheckoutBookForCustomer(book, customer);
        }
    }

    private void CheckInMenu(Book book) {
        var checkin = booleanPrompt("Do you want to return Book?");

        if (checkin) {
            library.CheckinBook(book);
        }
    }

    private Boolean booleanPrompt(string message) {
        return AnsiConsole.Prompt(
            new TextPrompt<bool>(message)
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "y" : "n"));
    }

    private static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }

}