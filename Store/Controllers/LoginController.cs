using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data;

namespace WebApplicationL5.Controllers;

[Route("Login")]
public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

   // [HttpPost]
   // public async Task<IActionResult> Login(string username, string password)
   // {
   //     // Здесь нет проверки на имя пользователя и пароль, пользователь авторизуется без них
   //     var claims = new List<Claim>
   //     {
   //         new Claim(ClaimTypes.Name, username ?? "guest"), // Можно использовать имя по умолчанию
   //         // Добавьте другие претензии, если необходимо
   //     };
//
   //     var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
   //     var principal = new ClaimsPrincipal(identity);
//
   //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
//
   //     return Redirect("/OrderController/"); // Перенаправление после успешной авторизации
   // }
}