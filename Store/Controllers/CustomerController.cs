using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Data.EF;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Customer")]
public class CustomerController : Controller
{
    //static readonly string _connectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    //private AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(_connectionString);
    private EFDataContext _efDataContext = new EFDataContext();
    
    private static int _id;


    public IActionResult Index()
    {
        //_id = _dataContext.GetCustomerId() + 1;
        _id = _efDataContext.GetCustomerId() + 1;
        //return View(_dataContext.SelectCustomers());
        return View(_efDataContext.SelectCustomers());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [Route("add")]
    public IActionResult Add([FromBody] Customer customer)
    {
        //_dataContext.AddCustomer(customer);
        _efDataContext.AddCustomer(customer);

        _id = customer.Id;

        return Ok(new {customer.Id });
    }

    [Route("update")]
    public IActionResult Update([FromBody] Customer updatedCustomer)
    {
        if (updatedCustomer.Id >= _id)
        {
            Add(updatedCustomer);
            return Ok();
        }
        
        //_dataContext.UpdateCustomer(updatedCustomer);
        _efDataContext.UpdateCustomer(updatedCustomer);
        return Ok();
        
    }
    
    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        //_dataContext.DeleteCustomer(id);
        _efDataContext.DeleteCustomer(id);

        return RedirectToAction("Index");
    }

    [Route("rows-count")]
    public IActionResult GetRowCount()
    {
        //var rows = _dataContext.CustomersRowsCount();
        var rows = _efDataContext.CustomersRowsCount();
        
        return Ok(rows);
    }
}