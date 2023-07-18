using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class TableController : Controller
{
    private static List<Product> _products = new();
    private static int _id = 0;

    private readonly string[] _items =
    {
        "Книга", "Журнал", "Плакат", "Словарь", "Ручка", "Ластик", "Гречка", "Носки", "DVD диск"
    };

    private readonly string[] _descriptions =
    {
        "Три товарища", "Охотник и рыбалов", "Молодой Леонтьев", "Польско-Русский", "Красная",
        "Обыкновенный", "Умный выбор", "Брестские", "Windows XP"
    };


    public IActionResult Index()
    {
        if (_id == 0) CreateFirstFiveProducts();
        
        return View(_products);
    }

    [HttpPost("add-product")]
    public IActionResult AddProduct(string name, string description, double price, int amount)
    {
        var product = new Product
        {
            Id = _id,
            Name = name,
            Description = description,
            Price = price,
            Amount = amount
        };

        _id++;
        _products.Add(product);

        return RedirectToAction("Index");
    }

    public IActionResult UpdateProduct(int productId, string newName, string newDescription, double newPrice, int newAmount)
    {
        _products[productId].Name = newName;
        _products[productId].Description = newDescription;
        _products[productId].Price = newPrice;
        _products[productId].Amount = newAmount;
        
        return RedirectToAction("Index");
    }

    public IActionResult DeleteProduct(int productId)
    {
        var index = _products.FindIndex(p => p.Id == productId);
        _products.RemoveAt(index);

        return RedirectToAction("Index");
    }

    private void CreateFirstFiveProducts()
    {
        var random = new Random();

        for (var i = 0; i < 5; i++)
        {
            var index = random.Next(_items.Length);
            var name = _items[index];
            var description = _descriptions[index];
            var price = Math.Round(random.NextDouble() * 100, 2);
            var amount = random.Next(0, 100);

            AddProduct(name, description, price, amount);
        }
    }
}