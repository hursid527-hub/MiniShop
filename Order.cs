using System.Linq;
using System.Collections.Generic;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();

    public decimal Total => Items.Sum(i => i.Total);

    public override string ToString()
    {
        var items = string.Join(", ", Items.Select(i => i.ToString()));
        return $"Order Id={Id}, Customer={CustomerName}, Items=[{items}], Total={Total:C}";
    }
}
