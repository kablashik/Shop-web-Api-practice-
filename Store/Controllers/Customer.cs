using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class CustomerController : Controller
{
    private static int Id = 0;

    private static List<Customer> _customers = new()
    {
        new Customer { Id = 1, FirstName = "Иван", LastName = "Петров", Age = 22, Country = "Россия" },
        new Customer { Id = 2, FirstName = "Елена", LastName = "Сидорова", Age = 30, Country = "Украина" },
        new Customer { Id = 3, FirstName = "Алексей", LastName = "Иванов", Age = 28, Country = "Беларусь" },
        new Customer { Id = 4, FirstName = "Мария", LastName = "Смирнова", Age = 25, Country = "Казахстан" },
        new Customer { Id = 5, FirstName = "Андрей", LastName = "Козлов", Age = 40, Country = "Россия" },
        new Customer { Id = 6, FirstName = "Ольга", LastName = "Морозова", Age = 35, Country = "Украина" },
        new Customer { Id = 7, FirstName = "Дмитрий", LastName = "Лебедев", Age = 29, Country = "Россия" },
        new Customer { Id = 8, FirstName = "Екатерина", LastName = "Соколова", Age = 27, Country = "Казахстан" },
        new Customer { Id = 9, FirstName = "Сергей", LastName = "Новиков", Age = 33, Country = "Беларусь" },
        new Customer { Id = 10, FirstName = "Анна", LastName = "Зайцева", Age = 31, Country = "Россия" }
    };

    public IActionResult Index()
    {
        return Index();
    }
}