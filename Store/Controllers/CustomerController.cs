using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

[Route("Customer")]
public class CustomerController : Controller
{
    private static int Id = 11;

    private static List<Customer> _customers = new()
    {
        new Customer { Id = 1, FirstName = "Иван", LastName = "Петров", Age = 22, Country = "Россия" },
        new Customer { Id = 2, FirstName = "Елена", LastName = "Сидорова", Age = 30, Country = "Украина" },
        new Customer { Id = 3, FirstName = "Алексей", LastName = "Иванов", Age = 28, Country = "Канада" },
        new Customer { Id = 4, FirstName = "Мария", LastName = "Смирнова", Age = 25, Country = "Казахстан" },
        new Customer { Id = 5, FirstName = "Андрей", LastName = "Козлов", Age = 40, Country = "Россия" },
        new Customer { Id = 6, FirstName = "Ольга", LastName = "Морозова", Age = 35, Country = "Украина" },
        new Customer { Id = 7, FirstName = "Дмитрий", LastName = "Лебедев", Age = 29, Country = "США" },
        new Customer { Id = 8, FirstName = "Екатерина", LastName = "Соколова", Age = 27, Country = "Румыния" },
        new Customer { Id = 9, FirstName = "Сергей", LastName = "Новиков", Age = 33, Country = "Беларусь" },
        new Customer { Id = 10, FirstName = "Анна", LastName = "Зайцева", Age = 31, Country = "Польша" }
    };

    public IActionResult Index()
    {
        return View(_customers);
    }

    [Route("add")]
    public IActionResult Add([FromBody] Customer customer)
    {
        customer.Id = Id;
        Id++;
        _customers.Add(customer);
        
        return Ok(new { id = customer.Id });
    }

    [Route("update-{id}")]
    public IActionResult Update(int id, [FromBody] Customer updatedCustomer)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Age = updatedCustomer.Age;
            customer.Country = updatedCustomer.Country;
            
            return Ok();
        } 
        
        return NotFound();
    }
    
    [HttpGet("delete-{id}")]
    public IActionResult Delete(int id)
    {
        var index = _customers.FindIndex(p => p.Id == id);
        _customers.RemoveAt(index);

        return RedirectToAction("Index");
    }

}