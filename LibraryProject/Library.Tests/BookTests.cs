namespace Library.Tests;

using Library;
public class BookTests {

    LibraryInventory library;

    public BookTests() {
        library = LibraryInventory.getInstance();
    }

    [Fact]
    public void AddBookTest() {
        // given
        File.Delete("book-data.txt");
        Book book = new Book("title", "genre", "isbn", "description", null, null);

        // when
        library.AddBook(book);

        // then
        var contentFromFile = File.ReadAllText("book-data.txt");
        Console.WriteLine(contentFromFile);
        Assert.Equal("title:genre:isbn:description::"+Environment.NewLine, contentFromFile);
    }
}