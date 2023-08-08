using System.Text.Json;
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


    [HttpPost("add-product")]
    public IActionResult AddProduct([FromForm] [FromBody] Product product)
    {
        product.ProductId = _id;
        _id++;
        _products.Add(product);

        return RedirectToAction("Index");
    }

    [HttpPost("add-product-json")]
    public IActionResult AddProductJson([FromBody] Product product)
    {
        product.ProductId = _id;
        _id++;
        _products.Add(product);

        return Ok(new { id = product.ProductId });
    }

    [HttpPost("update-product-json/{id}")]
    public IActionResult UpdateProductJson(int id, [FromBody] Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.ProductId == id);
        if (product != null)
        {
            product.ProductName = updatedProduct.ProductName;
            product.ProductDescription = updatedProduct.ProductDescription;
            product.ProductPrice = updatedProduct.ProductPrice;
            product.ProductAmount = updatedProduct.ProductAmount;
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
    
    [HttpGet("delete-{productId}")]
    public IActionResult DeleteProduct(int productId)
    {
        var index = _products.FindIndex(p => p.ProductId == productId);
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

            AddProduct(new Product() { ProductName = name, ProductDescription = description, ProductPrice = price, ProductAmount = amount });
        }
    }
}