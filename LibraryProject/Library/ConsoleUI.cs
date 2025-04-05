namespace Library;

using Spectre.Console;

public class ConsoleUI {
    
    BookService bookService;

    public ConsoleUI() {
        bookService = new BookService();
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

        // Echo the fruit back to the terminal
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
        Book book = new Book();

        book.title = AskForInput("Enter title: ");
        book.genre = AskForInput("Enter genre: ");
        book.isbn = AskForInput("Enter isbn: ");
        book.description = AskForInput("Enter description: ");

        bookService.AddBook(book);

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
        // TODO Add show Book
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }

}