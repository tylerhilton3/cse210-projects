public class Customer {
    private string name;
    private Address address;

    public Customer(string name, Address address) {
        this.name = name;
        this.address = address;
    }

    public bool IsInUSA() => address.IsInUSA();
    public string GetShippingLabel() => $"{name}\n{address.GetFullAddress()}";
}