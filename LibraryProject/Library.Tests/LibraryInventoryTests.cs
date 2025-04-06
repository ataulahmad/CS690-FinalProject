namespace Library.Tests;

using Library;

public class LibraryInventoryTests {

    LibraryInventory library;

    public LibraryInventoryTests() {
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
        Assert.Equal("title:genre:isbn:description::"+Environment.NewLine, contentFromFile);
    }


    [Fact]
    public void GetBookTest() {
        // given
        File.Delete("book-data.txt");
        Book expectedBook = new Book("title1", "genre", "isbn", "description", null, null);
        library.AddBook(expectedBook);

        // when
        Book? actualBook = library.GetBook("title1");

        // then
        Assert.Equal(expectedBook, actualBook);
    }

}