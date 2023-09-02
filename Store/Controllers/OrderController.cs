using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;
using WebApplicationL5.Data.EF;
using WebApplicationL5.ModelMappers;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    private readonly EFDataContext _efDataContext;
    private readonly IOrderModelMapper _orderModelMapper;

    private static int _id;

    public OrderController(EFDataContext dataContext, IOrderModelMapper modelMapper)
    {
         _efDataContext = dataContext;
         _orderModelMapper = modelMapper;
    }

    public IActionResult Index()
    {
        _id = _efDataContext.GetOrderId() + 1;
        return View(_efDataContext.SelectOrders());
    }

    [Route("id")]
    public IActionResult GetCurrentId()
    {
        return Content(_id.ToString());
    }

    [Route("add")]
    public IActionResult Add([FromBody] OrderModel orderModel)
    {
        var order = _orderModelMapper.MapFromModel(orderModel);
        _efDataContext.AddOrder(order);
        _id = orderModel.Id;

        return Ok(new { orderModel.Id });
    }

    [Route("update")]
    public IActionResult Update([FromBody] OrderModel updatedOrderModel)
    {
        if (updatedOrderModel.Id >= _id)
        {
            Add(updatedOrderModel);
            return Ok();
        }

        var order = _orderModelMapper.MapFromModel(updatedOrderModel);
        _efDataContext.UpdateOrder(order);
        return Ok();
    }

    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        _efDataContext.DeleteOrder(id);

        return RedirectToAction("Index");
    }

    [Route("rows-count")]
    public IActionResult GetRowsCount()
    {
        var rows = _efDataContext.OrdersRowsCount();

        return Ok(rows);
    }
}