namespace Library;

public class Customer {

    public string Name {get; set;}
    public string Address {get; set;}
    public string Email {get; set;}

    public Customer(string name, string address, string email) {
        this.Name = name;
        this.Address = address;
        this.Email = email;
    }

    public string CreateLineFromCustomer() {
        return $"{Name}:{Address}:{Email}";
    }


    public override string ToString() {
        return Name;
    }


    public override bool Equals(object? obj) {
        if (obj is not Customer other) return false;

        return string.Equals(Name, other.Name) &&
               string.Equals(Address, other.Address) &&
               string.Equals(Email, other.Email);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Name, Address, Email);
    }
    
}