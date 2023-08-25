using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApplicationL5.Data;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class ProductController : Controller
{
    private const string ConnectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    private readonly AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(ConnectionString);

    private static int _id;

    public IActionResult Index()
    {
        _id = _dataContext.GetProductId() + 1;

        return View(_dataContext.SelectProducts());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }
    

    [HttpPost("add")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        _dataContext.AddProduct(product);
        _id = product.Id;

        return Ok(new { product.Id });
    }

    [HttpPost("update")]
    public IActionResult UpdateProduct([FromBody] Product updatedProduct)
    {
        if (updatedProduct.Id >= _id)
        {
            AddProduct(updatedProduct);
            return Ok();
        }

        _dataContext.UpdateProduct(updatedProduct);

        return Ok();
    }

    [HttpGet("delete-{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _dataContext.DeleteProduct(id);

        return RedirectToAction("Index");
    }

    [Route("rows-count")]
    public IActionResult GetRowsCount()
    {
        var rows = _dataContext.ProductsRowsCount();

        return Ok(rows);
    }
}