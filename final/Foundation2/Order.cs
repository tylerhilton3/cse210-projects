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