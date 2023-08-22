using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApplicationL5.Data;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class ProductController : Controller
{
    private const string ConnectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    private readonly AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(ConnectionString);

    private static int _id = 0;

    public IActionResult Index()
    {
        return View(_dataContext.SelectProducts());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    private static void UpdateIdFromDb()
    {
        var currentId = -1;

        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        var query =
            "SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'usersdb' AND TABLE_NAME = 'product'";
        using var command = new MySqlCommand(query, connection);
        var result = command.ExecuteScalar();
        if (result != null && result != DBNull.Value)
        {
            currentId = Convert.ToInt32(result);
        }

        _id = currentId;
    }


    [HttpPost("add")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        UpdateIdFromDb();
        product.Id = _id;
        _id++;
        _dataContext.AddProduct(product);

        return Ok(new { product.Id });
    }

    [HttpPost("update-{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        UpdateIdFromDb();
        
        if (id >= _id)
        {
            AddProduct(updatedProduct);

            return Ok();
        }

        _dataContext.UpdateProduct(id, updatedProduct);

        return NotFound();
    }

    [HttpGet("delete-{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _dataContext.DeleteProduct(id);

        return RedirectToAction("Index");
    }
}