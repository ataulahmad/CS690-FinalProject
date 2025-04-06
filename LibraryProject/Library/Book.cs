namespace Library;

using Spectre.Console;

public class Book {

    public string title { get; set; }
    public string genre { get; set; }
    public string isbn { get; set; }
    public string description { get; set; }
    public Customer? customer { get; set; }
    public string? dueDate { get; set; }

    public Book(string title, string genre, string isbn, string description, Customer? customer, string? dueDate) {
        this.title = title;
        this.genre = genre;
        this.isbn = isbn;
        this.description = description;
        this.customer = customer;
        this.dueDate = dueDate;
    }

    public override string ToString() {
        return title;
    }

    public string CreateLineFromBook() {
        return title + ":" + genre + ":" + isbn + ":" + description + ":" + customer + ":" + dueDate;
    }

    public void printBookDetails() {
        // Create a table
        var table = new Table();

        // Add some columns
        table.AddColumn("Title");
        table.AddColumn("Genre");
        table.AddColumn("ISBN");
        table.AddColumn("Description");
        table.AddColumn("Customer");
        table.AddColumn("Due Date");

        string customerName = "";
        if (customer != null) {
            customerName = customer.Name;
        }

        string dueDateStr = "";
        if (dueDate != null) {
            dueDateStr = dueDate;
        }
        // Add some rows
        table.AddRow("[green]" + title + "[/]", genre, isbn, description, customerName, dueDateStr);

        // Render the table to the console
        AnsiConsole.Write(table);
    }

    
}