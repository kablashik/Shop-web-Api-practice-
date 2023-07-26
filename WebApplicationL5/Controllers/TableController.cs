using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class TableController : Controller
{
    public static List<Product> _products = new();
    private static int _id;

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

    [HttpGet("add-product")]
    public IActionResult AddProduct()
    {
        return View("IndexForm");
    }

    [HttpPost("add-product")]
    public IActionResult AddProduct([FromForm] string name, [FromForm] string description,
        [FromForm] double price, [FromForm] int amount)
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

    [HttpGet("update-product-{productId}")]
    public IActionResult UpdateProduct(int productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);

        return View("IndexForm", product);
    }

    [HttpPost("update-product-{productId}")]
    public IActionResult UpdateProduct([FromRoute] int productId, [FromForm] string? name,
        [FromForm] string? description, [FromForm] double price, [FromForm] int amount)
    {
        var index = _products.FindIndex(p => p.Id == productId);

        _products[index].Name = name ?? _products[index].Name;
        _products[index].Description = description ?? _products[index].Description;
        _products[index].Price = price != 0 ? price : _products[index].Price;
        _products[index].Amount = amount != 0 ? amount : _products[index].Amount;

        return RedirectToAction("Index");
    }

    [HttpGet("delete-{productId}")]
    public IActionResult DeleteProduct(int productId)
    {
        var index = _products.FindIndex(p => p.Id == productId);
        _products.RemoveAt(index);

        return RedirectToAction("Index");
    }

    public void CreateFirstFiveProducts()
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