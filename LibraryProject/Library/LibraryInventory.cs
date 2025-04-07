namespace Library;

public class LibraryInventory {

    private static LibraryInventory? instance;

    FileSaver librarianFileSaver;
    List<Librarian> librarians;

    FileSaver booksFileSaver;
    List<Book> books;

    FileSaver customerFileSaver;
    List<Customer> customers;

    private LibraryInventory() {
        initLibrarians();
        initCustomers();
        intitBooks();
    }

    private void initLibrarians() {
        librarians = new List<Librarian>();
        librarianFileSaver = new FileSaver("user-data.txt");
        
        foreach (string line in librarianFileSaver.GetAllLines()) {
            string[] attributes = line.Split(':');
            Librarian librarian = new Librarian(attributes[0], attributes[1]);
            librarians.Add(librarian);
        }
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

    public Boolean Login(string username, string password) {
        foreach (Librarian librarian in librarians) {
            if (librarian.username == username & librarian.password == password) {
                return true;
            }
        }
        return false;
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

    public List<Book> GetAllBooks() {
        return this.books;
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
        book.dueDate = DateTime.UtcNow.Date.AddDays(30).ToString("d");
        book.customer = customer;
        
        reloadBooks();
    }

    public void CheckinBook(Book book) {
        book.customer = null;
        book.dueDate = null;

        reloadBooks();
    }

    private void reloadBooks() {
        booksFileSaver.EmptyFile();
        
        foreach (Book b in books) {
            booksFileSaver.AppendLine(b.CreateLineFromBook());
        }
    }

}