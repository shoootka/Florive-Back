using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Florive.BusinessLogic;
using Florive.DataAccess;

namespace Florive.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionKey = context.HttpContext.Request.Cookies["X-KEY"];

            if (string.IsNullOrEmpty(sessionKey))
            {
                context.Result = new UnauthorizedObjectResult(
                    new { IsSuccess = false, Message = "Нет сессии" });
                return;
            }

            var dbContext = context.HttpContext.RequestServices
                .GetRequiredService<AppDbContext>();

            var bl = new BusinessLogic.BusinessLogic(dbContext);
            var result = bl.GetSessionActions().ValidateSessionAction(sessionKey);

            if (!result.IsSuccess)
            {
                context.Result = new UnauthorizedObjectResult(
                    new { IsSuccess = false, Message = result.Message });
            }
        }
    }
}