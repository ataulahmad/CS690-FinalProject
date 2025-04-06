namespace Library;

using Spectre.Console;

public class ConsoleUI {
    
    LibraryInventory library;

    public ConsoleUI() {
        library = LibraryInventory.getInstance();
    }

    public void MainMenu() {
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

    public void AddBookMenu() {
        Console.WriteLine("=== Add Book ===");

        string title = AskForInput("Enter title: ");
        string genre = AskForInput("Enter genre: ");
        string isbn = AskForInput("Enter isbn: ");
        string description = AskForInput("Enter description: ");

        Book book = new Book(title, genre, isbn, description, null, null);

        library.AddBook(book);

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>("Add new Book?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "y" : "n"));
            
        if (confirmation) {
            AddBookMenu();
        } else {
            MainMenu();
        }
    }

    public void ShowBookMenu() {
        Console.WriteLine("=== Show Book ===");

        string? searchTitle = AskForInput("Enter books title: ");

        Book? book = library.GetBook(searchTitle);

        if (book != null) {
            book.printBookDetails();
        } else {
            Console.WriteLine("No Book found!");
        }

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>("Search another Book?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "y" : "n"));
            
        if (confirmation) {
            ShowBookMenu();
        } else {
            MainMenu();
        }
    }

    public static string? AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }

}