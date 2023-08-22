using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    static readonly string _connectionString = "Server=localhost;Database=usersdb;Uid=root;Pwd=3079718;";
    private AdoConnectedDataContext _dataContext = new AdoConnectedDataContext(_connectionString);

    private static int _id = 11;
    //private static List<Order> _orders = new ()
    //{
    //    new Order {Id = 1, CustomerId = 2, ProductId = 2, Count = 5, CreatedAt = new DateTime(2023, 08, 1)},
    //    new Order {Id = 2, CustomerId = 1, ProductId = 5, Count = 1, CreatedAt = new DateTime(2023, 08, 4)},
    //    new Order {Id = 3, CustomerId = 5, ProductId = 5, Count = 8, CreatedAt = new DateTime(2023, 06, 15)},
    //    new Order {Id = 4, CustomerId = 9, ProductId = 1, Count = 1, CreatedAt = new DateTime(2023, 05, 15)},
    //    new Order {Id = 5, CustomerId = 4, ProductId = 3, Count = 12, CreatedAt = new DateTime(2023, 03, 12)},
    //    new Order {Id = 6, CustomerId = 3, ProductId = 6, Count = 2, CreatedAt = new DateTime(2023, 04, 21)},
    //    new Order {Id = 7, CustomerId = 1, ProductId = 3, Count = 1, CreatedAt = new DateTime(2023, 07, 9)},
    //    new Order {Id = 8, CustomerId = 4, ProductId = 2, Count = 15, CreatedAt = new DateTime(2023, 08, 9)},
    //    new Order {Id = 9, CustomerId = 6, ProductId = 7, Count = 2, CreatedAt = new DateTime(2023, 05, 2)},
    //    new Order {Id = 10, CustomerId = 10, ProductId = 10, Count = 3, CreatedAt = new DateTime(2023, 03, 1)},
    //};

    public IActionResult Index()
    {
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
        order.Id = _id;
        _id++;
        _dataContext.AddOrder(order);
        //_orders.Add(order);

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
       //var index = _orders.FindIndex(p => p.Id == id);
       //_orders.RemoveAt(index);

        return RedirectToAction("Index");
    }

}