namespace Library;

public class Library {

    private static Library instance;

    FileSaver booksFileSaver;
    List<Book> books;

    private Library() {
        books = new List<Book>();
        booksFileSaver = new FileSaver("book-data.txt");
        
        foreach (string line in booksFileSaver.GetAllLines()) {
            books.Add(Book.CreateBook(line));
        }
    }

    public static Library getInstance() {
        if (instance == null) {
            instance = new Library();
        }
        return instance;
    }

    public void AddBook(Book book) {
        booksFileSaver.AppendLine(book.DbString());
        books.Add(book);
    }

    public Book GetBook(string searchTitle) {
        foreach (Book book in this.books) {
            if (book.title == searchTitle) {
                return book;
            }
        }
        return null;
    }

}