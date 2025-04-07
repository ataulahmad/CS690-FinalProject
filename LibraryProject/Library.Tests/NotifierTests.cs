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
        Book book2 = new Book("title2", "genre", "isbn", "description", null, null);
        library.AddBook(book);
        library.AddBook(book2);
        library.CheckoutBookForCustomer(book, customer);
        
        book.dueDate = "01/01/2025";

        // when
        int sentNotifications = notifier.CheckAndNotify();

        // then
        Assert.Equal(1, sentNotifications);
    }

}