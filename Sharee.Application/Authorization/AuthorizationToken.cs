using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sharee.Application.Data;

namespace Sharee.Application.Authorization;

public class AuthorizationToken : ActionFilterAttribute, IAuthorizationFilter
{
    private static readonly IActionResult DefaultResult = new ContentResult()
    {
        StatusCode = StatusCodes.Status401Unauthorized
    };

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ShareeDbContext>();

        if (!Guid.TryParse(context.HttpContext.Request.Query["token"], CultureInfo.InvariantCulture, out var guid) && 
            dbContext.Units.Any(unit => unit.Token.Equals(guid)))
        {
            context.Result = DefaultResult;
        }
    }
}