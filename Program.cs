internal class Program
{
    private static void Main(string[] args)
    {
        var store = new DataStore();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== MINI SHOP =====");
            Console.WriteLine();
            Console.WriteLine("1. Mahsulotlarni ko'rish");
            Console.WriteLine("2. Mahsulot qo'shish");
            Console.WriteLine("3. Mahsulotni o'chirish");
            Console.WriteLine("4. Mahsulotni qidirish");
            Console.WriteLine("5. Buyurtma berish");
            Console.WriteLine("6. Buyurtmalarni ko'rish");
            Console.WriteLine("0. Chiqish");
            Console.Write("Tanlov: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListProducts(store);
                    break;
                case "2":
                    AddProduct(store);
                    break;
                case "3":
                    DeleteProduct(store);
                    break;
                case "4":
                    SearchProduct(store);
                    break;
                case "5":
                    PlaceOrder(store);
                    break;
                case "6":
                    ListOrders(store);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Noto'g'ri tanlov.");
                    break;
            }
        }
    }

    private static void ListProducts(DataStore store)
    {
        Console.WriteLine("\nMahsulotlar:");
        foreach (var p in store.GetProducts())
            Console.WriteLine(p);
    }

    private static void AddProduct(DataStore store)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        decimal price;
        while (true)
        {
            Console.Write("Price: ");
            var s = Console.ReadLine();
            if (decimal.TryParse(s, out price)) break;
            Console.WriteLine("Iltimos to'g'ri son kiriting (masalan 12.5).");
        }

        int qty;
        while (true)
        {
            Console.Write("Quantity: ");
            var s = Console.ReadLine();
            if (int.TryParse(s, out qty)) break;
            Console.WriteLine("Iltimos butun son kiriting.");
        }

        var p = store.AddProduct(name, price, qty);
        Console.WriteLine("Qo'shildi: " + p);
    }

    private static void DeleteProduct(DataStore store)
    {
        Console.Write("O'chirish uchun Id: ");
        var s = Console.ReadLine();
        if (int.TryParse(s, out var id))
        {
            if (store.DeleteProduct(id)) Console.WriteLine("O'chirildi.");
            else Console.WriteLine("Id topilmadi.");
        }
        else Console.WriteLine("Noto'g'ri Id.");
    }

    private static void SearchProduct(DataStore store)
    {
        Console.Write("Qidiruv so'zi: ");
        var term = Console.ReadLine() ?? string.Empty;
        var results = store.SearchProducts(term);
        Console.WriteLine("Natijalar:");
        foreach (var p in results) Console.WriteLine(p);
    }

    private static void PlaceOrder(DataStore store)
    {
        Console.Write("Buyurtmachi ismi: ");
        var customer = Console.ReadLine() ?? string.Empty;

        var items = new List<(int productId, int qty)>();
        while (true)
        {
            Console.Write("Mahsulot Id (y -> yakunlash): ");
            var s = Console.ReadLine();
            if (s?.Trim().ToLower() == "y") break;
            if (!int.TryParse(s, out var pid)) { Console.WriteLine("Noto'g'ri Id."); continue; }
            Console.Write("Soni: ");
            var ss = Console.ReadLine();
            if (!int.TryParse(ss, out var qty)) { Console.WriteLine("Noto'g'ri son."); continue; }
            items.Add((pid, qty));
        }

        var order = store.PlaceOrder(customer, items);
        if (order == null) Console.WriteLine("Buyurtma joylanmadi (ba'zi Idlar topilmadi).");
        else Console.WriteLine("Buyurtma qabul qilindi:\n" + order);
    }

    private static void ListOrders(DataStore store)
    {
        Console.WriteLine("\nBuyurtmalar:");
        foreach (var o in store.GetOrders()) Console.WriteLine(o);
    }
}
