using System.Collections.Generic;
using System.Linq;

public class DataStore
{
    private readonly List<Product> _products = new();
    private readonly List<Order> _orders = new();
    private int _nextProductId = 1;
    private int _nextOrderId = 1;

    public IEnumerable<Product> GetProducts() => _products.ToArray();

    public Product AddProduct(string name, decimal price, int quantity)
    {
        var p = new Product 
        {
            Id = _nextProductId++, Name = name, Price = price, Quantity = quantity
        };
        _products.Add(p);
        return p;
    }

    public bool DeleteProduct(int id)
    {
        var p = _products.FirstOrDefault(x => x.Id == id);
        if (p == null)
        {
            return false;
        }
        _products.Remove(p);
        return true;
    }

    public IEnumerable<Product> SearchProducts(string term)
    {
        if (string.IsNullOrWhiteSpace(term)) 
            return Enumerable.Empty<Product>();
        return _products.Where(p => p.Name?.IndexOf(term, System.StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public Order? PlaceOrder(string customerName, List<(int productId, int qty)> items)
    {
        var order = new Order 
        { 
            Id = _nextOrderId++, CustomerName = customerName
        };
        foreach (var it in items)
        {
            var p = _products.FirstOrDefault(x => x.Id == it.productId);
            if (p == null)
            {
                return null;
            }
            order.Items.Add(new OrderItem 
            {
                ProductId = p.Id, ProductName = p.Name, Price = p.Price, Quantity = it.qty 
            });
        }
        _orders.Add(order);
        return order;
    }

    public IEnumerable<Order> GetOrders() => _orders.ToArray();
}
