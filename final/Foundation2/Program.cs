using System;
using System.Collections.Generic;

public class Product {
    private string name;
    private string productId;
    private decimal pricePerUnit;
    private int quantity;

    public Product(string name, string productId, decimal pricePerUnit, int quantity) {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public string GetName() => name;
    public string GetProductId() => productId;
    public decimal GetTotalCost() => pricePerUnit * quantity;
}

public class Address {
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country) {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA() => country.ToUpper() == "USA";
    public string GetFullAddress() => $"{street}\n{city}, {state}\n{country}";
}

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

public class Order {
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer) {
        this.customer = customer;
        products = new List<Product>();
    }

    public void AddProduct(Product product) {
        products.Add(product);
    }

    public decimal GetTotalPrice() {
        decimal total = 0;
        foreach (var product in products) {
            total += product.GetTotalCost();
        }
        total += customer.IsInUSA() ? 5 : 35; // Shipping cost
        return total;
    }

    public string GetPackingLabel() {
        string label = "";
        foreach (var product in products) {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel() {
        return customer.GetShippingLabel();
    }
}

public class Program {
    public static void Main() {
        var customer1 = new Customer("John Doe", new Address("123 Elm St", "Springfield", "IL", "USA"));
        var customer2 = new Customer("Jane Smith", new Address("456 Oak St", "Vancouver", "BC", "Canada"));

        var order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "L123", 999.99m, 1));
        order1.AddProduct(new Product("Mouse", "M456", 25.99m, 2));

        var order2 = new Order(customer2);
        order2.AddProduct(new Product("Desk", "D789", 199.99m, 1));
        order2.AddProduct(new Product("Chair", "C012", 89.99m, 2));

        Console.WriteLine("Order 1 Total Price: " + order1.GetTotalPrice());
        Console.WriteLine("Order 1 Packing Label:\n" + order1.GetPackingLabel());
        Console.WriteLine("Order 1 Shipping Label:\n" + order1.GetShippingLabel());

        Console.WriteLine("Order 2 Total Price: " + order2.GetTotalPrice());
        Console.WriteLine("Order 2 Packing Label:\n" + order2.GetPackingLabel());
        Console.WriteLine("Order 2 Shipping Label:\n" + order2.GetShippingLabel());
    }
}
