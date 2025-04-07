namespace Library.Tests;

using Library;

public class NotifierTests {

    LibraryInventory library;
    Notifier notifier;

    public NotifierTests() {
        library = LibraryInventory.getInstance();
        notifier = new Notifier();
    }

    [Fact]
    public void SendNotificationTest() {
        // given
        File.Delete("book-data.txt");
        File.Delete("customer-data.txt");

        Customer customer = new Customer("name", "address", "email");
        library.AddCustomer(customer);
        Book book = new Book("title1", "genre", "isbn", "description", null, null);
        library.AddBook(book);
        library.CheckoutBookForCustomer(book, customer);
        
        book.dueDate = "01/01/2025";

        var originalOut = Console.Out; // Save the original output
        using var sw = new StringWriter();
        Console.SetOut(sw); // Redirect Console output

        // when
        notifier.CheckAndNotify();

        // then
        var expected = "Book title1 is over due. Sending email at email"+ Environment.NewLine;
        Assert.Equal(expected, sw.ToString());

        Console.SetOut(originalOut); // Restore it BEFORE StringWriter gets disposed
    }

}