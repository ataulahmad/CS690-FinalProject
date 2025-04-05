namespace Library;

public class BookService {

    FileSaver fileSaver;

    public BookService() {
        fileSaver = new FileSaver("book-data.txt");
    }

    public void AddBook(Book book) {
        fileSaver.AppendLine(book.title + ":" + book.genre + ":" + book.isbn + ":" + book.description + ":" + book.customer + ":" + book.dueDate);
    }


}