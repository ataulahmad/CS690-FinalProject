namespace Library;

public class BookService {

    FileSaver fileSaver;

    public BookService() {
        fileSaver = new FileSaver("book-data.txt");
    }

    public void AddBook(string title, string genre, string isbn, string description) {
        // TODO: Add Customer and due date 
        fileSaver.AppendLine(title + ":" + genre + ":" + isbn + ":" + description + ":" + null + ":" + null);
    }


}