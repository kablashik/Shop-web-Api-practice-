using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Models;

namespace WebApplicationL5.Controllers;

public class FormController : Controller
{
    private readonly ILogger<FormController> _logger;

    public FormController(ILogger<FormController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}