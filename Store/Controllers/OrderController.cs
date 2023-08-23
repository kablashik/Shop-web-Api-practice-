using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    static readonly string _connectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    private AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(_connectionString);

    private static int _id;

    public IActionResult Index()
    {
        _id = _dataContext.GetOrderId() + 1;
        return View(_dataContext.SelectOrders());
    }
    
    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [Route("add")]
    public IActionResult Add([FromBody] Order order)
    {
        _dataContext.AddOrder(order);
        _id = order.Id;

        return Ok(new { order.Id });
    }

    [Route("update-{id}")]
   public IActionResult Update(int id,[FromBody] Order updatedOrder)
   {
       if (id >= _id)
       {
           Add(updatedOrder);
           return Ok();
       }

       _dataContext.UpdateOrder(id, updatedOrder);
       return Ok();

       //var order = _orders.FirstOrDefault(o => o.Id == id);
       //if (order != null)
       //{
       //    order.CustomerId = updatedOrder.CustomerId;
       //    order.ProductId = updatedOrder.ProductId;
       //    order.Count = updatedOrder.Count;
       //   //order.CreatedAt = new DateTime(1991,03,1);
       //   order.CreatedAt = updatedOrder.CreatedAt;
       //    return Ok();
       //} 

       //return NotFound();

   }
    
    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        _dataContext.DeleteOrder(id);

        return RedirectToAction("Index");
    }

}