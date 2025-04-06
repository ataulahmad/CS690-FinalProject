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


    [Fact]
    public void AddCustomerTest() {
        // given
        File.Delete("customer-data.txt");
        Customer customer = new Customer("name", "address", "email");

        // when
        library.AddCustomer(customer);

        // then
        var contentFromFile = File.ReadAllText("customer-data.txt");
        Assert.Equal("name:address:email"+Environment.NewLine, contentFromFile);
    }


    [Fact]
    public void GetCustomerTest() {
        // given
        File.Delete("customer-data.txt");
        Customer expectedCustomer = new Customer("name1", "address", "email");
        library.AddCustomer(expectedCustomer);

        // when
        Customer? actualCustomer = library.GetCustomer("name1");

        // then
        Assert.Equal(expectedCustomer, actualCustomer);
    }


    [Fact]
    public void CheckOutBookForCustomerTest() {
        // given
        File.Delete("book-data.txt");
        File.Delete("customer-data.txt");

        Customer customer = new Customer("name", "address", "email");
        library.AddCustomer(customer);
        Book book = new Book("title1", "genre", "isbn", "description", null, null);
        library.AddBook(book);

        // when
        library.CheckoutBookForCustomer(book, customer);

        // then
        Assert.True(book.customer == customer);
        Assert.True(book.dueDate == DateTime.UtcNow.Date.AddDays(30).ToString("d"));
    }


    [Fact]
    public void CheckInTest() {
        // given
        File.Delete("book-data.txt");
        File.Delete("customer-data.txt");

        Customer customer = new Customer("name", "address", "email");
        library.AddCustomer(customer);
        Book book = new Book("title1", "genre", "isbn", "description", null, null);
        library.AddBook(book);

        library.CheckoutBookForCustomer(book, customer);

        // when
        library.CheckinBook(book);

        // then
        Assert.True(book.customer == null);
        Assert.True(book.dueDate == null);
    }

}