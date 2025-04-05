namespace Library.Tests;

using Library;
public class BookTests {

    BookService bookService;

    public BookTests() {
        bookService = new BookService();
    }

    [Fact]
    public void AddBookTest() {
        // given
        Book book = new Book();
        book.title = "title";
        book.genre = "genre";
        book.isbn = "isbn";
        book.description = "description";

        // when
        bookService.AddBook(book);

        // then
        var contentFromFile = File.ReadAllText("book-data.txt");
        Assert.Equal("title:genre:isbn:description::"+Environment.NewLine, contentFromFile);
    }
}