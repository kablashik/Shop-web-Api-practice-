using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Data.EF;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    //static readonly string _connectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    //private AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(_connectionString);
    private EFDataContext _efDataContext = new EFDataContext();

    private static int _id;

    public IActionResult Index()
    {
        //_id = _dataContext.GetOrderId() + 1;
        //return View(_dataContext.SelectOrders());
        _id = _efDataContext.GetOrderId() + 1;
        return View(_efDataContext.SelectOrders());
    }
    
    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [Route("add")]
    public IActionResult Add([FromBody] Order order)
    {
        //_dataContext.AddOrder(order);
        _efDataContext.AddOrder(order);
        _id = order.Id;

        return Ok(new { order.Id });
    }

    [Route("update")]
   public IActionResult Update([FromBody] Order updatedOrder)
   {
       if (updatedOrder.Id >= _id)
       {
           Add(updatedOrder);
           return Ok();
       }

       //_dataContext.UpdateOrder(updatedOrder);
       _efDataContext.UpdateOrder(updatedOrder);
       return Ok();
   }
    
    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        //_dataContext.DeleteOrder(id);
        _efDataContext.DeleteOrder(id);

        return RedirectToAction("Index");
    }
    
    [Route("rows-count")]
    public IActionResult GetRowsCount()
    {
        //var rows = _dataContext.OrdersRowsCount();
        var rows = _efDataContext.OrdersRowsCount();

        return Ok(rows);
    }
}