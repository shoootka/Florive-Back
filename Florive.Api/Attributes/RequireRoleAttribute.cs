using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Florive.DataAccess;
using System.Linq;

namespace Florive.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public RequireRoleAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionKey = context.HttpContext.Request.Cookies["X-KEY"];
            var dbContext = context.HttpContext.RequestServices
                .GetRequiredService<AppDbContext>();

            var session = dbContext.UserSessions
                .FirstOrDefault(s => s.SessionKey == sessionKey);

            var user = session != null
                ? dbContext.Users.FirstOrDefault(u => u.Id == session.UserId)
                : null;

            if (user?.Role != _role)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
        }
    }
}