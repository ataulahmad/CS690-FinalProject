namespace Library;

public class Notifier {

    LibraryInventory library = LibraryInventory.getInstance();

    public int CheckAndNotify() {
        int sentNotifications = 0;
        List<Book> books = library.GetAllBooks();
        foreach(Book book in books) {
            if (!string.IsNullOrEmpty(book.dueDate) && DateTime.Parse(book.dueDate) < DateTime.UtcNow.Date) {
                notifyCustomer(book);
                sentNotifications++;
            }
        }
        return sentNotifications;
    }

    private void notifyCustomer(Book book) {
        // This is just a sample output. This should be enhanced with a real email sending functionality. 
        // For this class it is out of scope.
        Console.WriteLine("Book " + book.title + " is over due. Sending email at " + book.customer.Email);
    }

}