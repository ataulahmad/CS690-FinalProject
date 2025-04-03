namespace Library;

public class ConsoleUI {
    
    FileSaver fileSaver;

    public ConsoleUI() {
        fileSaver = new FileSaver("book-data.txt");
    }

    public void show() {
        string command;

        do {
            Console.WriteLine("Add Book");
            string title = AskForInput("Enter title: ");
            string genre = AskForInput("Enter genre: ");
            string isbn = AskForInput("Enter isbn: ");
            string description = AskForInput("Enter description: ");

            fileSaver.AppendLine(title + ":" + genre + ":" + isbn + ":" + description);

            command = AskForInput("Add new Book? (Y / N): ");
        } while (command != "N");
    }

    public static string AskForInput(string message) {
        Console.WriteLine(message);
        return Console.ReadLine();
    }

}