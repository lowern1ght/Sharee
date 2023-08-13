using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sharee.Application.Data;

namespace Sharee.Application.Authorization;

public class AuthorizationToken : ActionFilterAttribute, IAuthorizationFilter
{
    private static readonly IActionResult DefaultResult = new ContentResult()
    {
        Content = "token query param null or uncorrected",
        StatusCode = StatusCodes.Status401Unauthorized
    };

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ShareeDbContext>();

        var token = context.HttpContext.Request.Query["token"];
        
        if (!Guid.TryParse(token, CultureInfo.InvariantCulture, out var guid) &&
            dbContext.Units.Any(unit => unit.Token.Equals(guid)))
        {
            context.Result = DefaultResult;
        }
    }
}