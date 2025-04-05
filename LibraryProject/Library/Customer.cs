namespace Library;

public class Customer {

    public string Name {get; set;}
    public string Address {get; set;}
    public string Email {get; set;}

    public override string ToString()
    {
        return Name;
    }
    
}