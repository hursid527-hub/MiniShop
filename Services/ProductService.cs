using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class ProductService
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;
    private readonly string _dataFile;

    public ProductService(string? dataFile = null)
    {
        _dataFile = dataFile ?? Path.Combine(AppContext.BaseDirectory, "products.json");
        Load();
    }

    public Product Add(string name, decimal price, int quantity)
    {
        var p = new Product
        {
            Id = _nextId++,
            Name = name,
            Price = price,
            Quantity = quantity
        };
        _products.Add(p);
        Save();
        return p;
    }

    public IEnumerable<Product> GetAll() => _products.ToArray();

    public IEnumerable<Product> SearchByName(string term)
    {
        if (string.IsNullOrWhiteSpace(term)) return Enumerable.Empty<Product>();
        return _products.Where(p => p.Name?.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    private void Load()
    {
        try
        {
            if (!File.Exists(_dataFile)) return;
            var json = File.ReadAllText(_dataFile);
            var items = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (items == null) return;
            _products.Clear();
            _products.AddRange(items);
            _nextId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        }
        catch
        {
            // ignore load errors, start with empty list
            _products.Clear();
            _nextId = 1;
        }
    }

    private void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(_dataFile);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_dataFile, json);
        }
        catch
        {
            // ignore save errors for simplicity
        }
    }
}
