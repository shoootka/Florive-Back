using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Florive.DataAccess;
using System.Linq;

namespace Florive.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
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

            var session = dbContext.UserSessions
                .FirstOrDefault(s => s.SessionKey == sessionKey);

            var user = session != null
                ? dbContext.Users.FirstOrDefault(u => u.Id == session.UserId)
                : null;

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(
                    new { IsSuccess = false, Message = "Пользователь не найден" });
                return;
            }

            if (user.Role != "Admin")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}