using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Customer")]
public class CustomerController : Controller
{
    static readonly string _connectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    private AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(_connectionString);
    
    private static int _id;


    public IActionResult Index()
    {
        _id = _dataContext.GetCustomerId() + 1;
        return View(_dataContext.SelectCustomers());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [Route("add")]
    public IActionResult Add([FromBody] Customer customer)
    {
        _dataContext.AddCustomer(customer);

        _id = customer.Id;

        return Ok(new {customer.Id });
    }

    [Route("update-{id}")]
    public IActionResult Update(int id, [FromBody] Customer updatedCustomer)
    {
        if (id >= _id)
        {
            Add(updatedCustomer);
            return Ok();
        }

        _dataContext.UpdateCustomer(id, updatedCustomer);
        return Ok();
        
    }
    
    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        _dataContext.DeleteCustomer(id);

        return RedirectToAction("Index");
    }

}