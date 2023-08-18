using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class ProductController : Controller
{
    private static int _id = 11;
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Гарри Поттер и философский камень", Description = "Магический роман", Price = 21.0, Type = ProductType.Книга },
        new Product { Id = 2, Name = "Яблоки", Description = "Свежие яблоки", Price = 1.5, Type = ProductType.Продукт },
        new Product { Id = 3, Name = "The Beatles: Abbey Road", Description = "Легендарный альбом", Price = 12.99, Type = ProductType.Диск },
        new Product { Id = 4, Name = "1984", Description = "Фантастический роман", Price = 18.75, Type = ProductType.Книга },
        new Product { Id = 5, Name = "Молоко", Description = "Пастеризованное молоко", Price = 2.25, Type = ProductType.Продукт },
        new Product { Id = 6, Name = "Thriller", Description = "Культовый альбом", Price = 14.5, Type = ProductType.Диск },
        new Product { Id = 7, Name = "Три товарища", Description = "Роман", Price = 25.99, Type = ProductType.Книга },
        new Product { Id = 8, Name = "Хлеб", Description = "Белый хлеб", Price = 1.0, Type = ProductType.Продукт },
        new Product { Id = 9, Name = "The Dark Side of the Moon", Description = "Икона прогрессивного рока", Price = 16.75, Type = ProductType.Диск },
        new Product { Id = 10, Name = "Гарри Поттер и узник Азкабана", Description = "Приключенческий роман", Price = 22.5, Type = ProductType.Книга }
    };
    
    public IActionResult Index()
    {
        return View(_products);
    }
    
    [HttpPost("add")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        product.Id = _id;
        _id++;
        _products.Add(product);

        return Ok(new {product.Id });
    }

    [HttpPost("update-{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            var name = string.IsNullOrEmpty(updatedProduct.Name) ?
                product.Name : updatedProduct.Name;
            var description = string.IsNullOrEmpty(updatedProduct.Description) ?
                product.Description : updatedProduct.Description;
            
            product.Name = name;
            product.Description = description;
            product.Price = updatedProduct.Price;
            product.Type = updatedProduct.Type;
            
            return Ok();
        } 
        
        return NotFound();
    }
    
    [HttpGet("delete-{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var index = _products.FindIndex(p => p.Id == id);
        _products.RemoveAt(index);

        return RedirectToAction("Index");
    }
}