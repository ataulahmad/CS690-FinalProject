namespace Library;

public class LibraryInventory {

    private static LibraryInventory? instance;

    FileSaver booksFileSaver;
    List<Book> books;

    private LibraryInventory() {
        books = new List<Book>();
        booksFileSaver = new FileSaver("book-data.txt");
        
        foreach (string line in booksFileSaver.GetAllLines()) {
            books.Add(Book.CreateBook(line));
        }
    }

    public static LibraryInventory getInstance() {
        if (instance == null) {
            instance = new LibraryInventory();
        }
        return instance;
    }

    public void AddBook(Book book) {
        booksFileSaver.AppendLine(book.DbString());
        books.Add(book);
    }

    public Book? GetBook(string searchTitle) {
        foreach (Book book in this.books) {
            if (book.title == searchTitle) {
                return book;
            }
        }
        return null;
    }

}