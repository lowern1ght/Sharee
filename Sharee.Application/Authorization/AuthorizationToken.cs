using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sharee.Application.Data;
using Sharee.Application.Services;

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
        
        var authorizationSession = context.HttpContext.RequestServices.GetService<AuthorizationSession>()!;

        if (!authorizationSession.CanAuthorizationOnSession(context.HttpContext.Session))
        {
            var token = context.HttpContext.Request.Query["token"];

            if (!Guid.TryParse(token, CultureInfo.InvariantCulture, out var guid) &&
                !dbContext.Units.Any(unit => unit.Token.Equals(guid)))
            {
                context.Result = DefaultResult;
            }
        }
    }
}