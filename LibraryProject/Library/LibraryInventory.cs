namespace Library;

public class LibraryInventory {

    private static LibraryInventory? instance;

    FileSaver booksFileSaver;
    List<Book> books;

    FileSaver customerFileSaver;
    List<Customer> customers;

    private LibraryInventory() {
        initCustomers();
        intitBooks();
    }

    private void initCustomers() {
        customers = new List<Customer>();
        customerFileSaver = new FileSaver("customer-data.txt");
        
        foreach (string line in customerFileSaver.GetAllLines()) {
            string[] attributes = line.Split(':');
            Customer customer = new Customer(attributes[0], attributes[1], attributes[2]);
            customers.Add(customer);
        }
    }

    private void intitBooks() {
        books = new List<Book>();
        booksFileSaver = new FileSaver("book-data.txt");
        
        foreach (string line in booksFileSaver.GetAllLines()) {
            string[] attributes = line.Split(':');
            Customer? customer = GetCustomer(attributes[4]);
            Book book = new Book(attributes[0], attributes[1], attributes[2], attributes[3], customer, attributes[5]);
            books.Add(book);
        }
    }

    public static LibraryInventory getInstance() {
        if (instance == null) {
            instance = new LibraryInventory();
        }
        return instance;
    }

    public void AddBook(Book book) {
        booksFileSaver.AppendLine(book.CreateLineFromBook());
        books.Add(book);
    }

    public Book? GetBook(string? searchTitle) {
        foreach (Book book in this.books) {
            if (book.title == searchTitle) {
                return book;
            }
        }
        return null;
    }

    public void AddCustomer(Customer customer) {
        customerFileSaver.AppendLine(customer.CreateLineFromCustomer());
        customers.Add(customer);
    }

    public Customer? GetCustomer(string? searchName) {
        foreach (Customer customer in this.customers) {
            if (customer.Name == searchName) {
                return customer;
            }
        }
        return null;
    }

    public void CheckoutBookForCustomer(Book book, Customer customer) {
        book.customer = customer;
        booksFileSaver.EmptyFile();
        
        foreach (Book b in books) {
            booksFileSaver.AppendLine(b.CreateLineFromBook());
        }
    }

}