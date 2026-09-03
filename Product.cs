public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"Id={Id}, Name={Name}, Price={Price:C}, Quantity={Quantity}";
    }
}
