using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplicationL5.Controllers;
using WebApplicationL5.Data.Models;

public class AgeAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public int MinimumAge { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        if (!httpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userName = httpContext.User.Identity.Name;
        var user = CookieController.GetCurrentUser(userName);

        if (user == null)
        {
            context.Result = new UnauthorizedResult(); // Пользователь не найден
            return;
        }

        if (user.Age < MinimumAge)
        {
            context.Result = new ForbidResult(); // Возвращаем результат с отказом в доступе
            return;
        }
    }
}