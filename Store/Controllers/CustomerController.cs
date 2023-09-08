using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Data.EF;
using WebApplicationL5.ModelMappers;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Customer")]
public class CustomerController : Controller
{
    private readonly EFDataContext _efDataContext;
    private readonly ICustomerModelMapper _customerModelMapper;
    private static int _id;

    public CustomerController(EFDataContext dataContext, ICustomerModelMapper modelMapper)
    {
        _efDataContext = dataContext;
        _customerModelMapper = modelMapper;
    }

    [AgeAuthorize(MinimumAge = 18)]
    [Authorize(Roles = "admin, user")]
    public IActionResult Index()
    {
        _id = _efDataContext.GetCustomerId() + 1;
        return View(_efDataContext.SelectCustomers());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [Route("add")]
    public IActionResult Add([FromBody] CustomerModel customer)
    {
        var dbCustomer = _customerModelMapper.MapFromModel(customer);
        _efDataContext.AddCustomer(dbCustomer);

        _id = customer.Id;

        return Ok(new { customer.Id });
    }

    [Route("update")]
    public IActionResult Update([FromBody] CustomerModel updatedCustomer)
    {
        if (updatedCustomer.Id >= _id)
        {
            Add(updatedCustomer);
            return Ok();
        }

        var dbCustomer = _customerModelMapper.MapFromModel(updatedCustomer);
        _efDataContext.UpdateCustomer(dbCustomer);

        return Ok();
    }

    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        _efDataContext.DeleteCustomer(id);

        return RedirectToAction("Index");
    }

    [Route("rows-count")]
    public IActionResult GetRowCount()
    {
        var rows = _efDataContext.CustomersRowsCount();

        return Ok(rows);
    }
}