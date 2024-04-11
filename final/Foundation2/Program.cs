using System;
using System.Collections.Generic;

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
