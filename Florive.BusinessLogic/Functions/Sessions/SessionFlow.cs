using Florive.BusinessLogic.Core.Users;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Functions.Sessions
{
    public class SessionFlow : SessionAction, ISession
    {
        public SessionFlow(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg CreateSessionAction(int userId)
        {
            return ExecuteCreateSessionAction(userId);
        }

        public ResponseMsg ValidateSessionAction(string sessionKey)
        {
            return ExecuteValidateSessionAction(sessionKey);
        }

        public ResponseMsg DeleteSessionAction(string sessionKey)
        {
            return ExecuteDeleteSessionAction(sessionKey);
        }
    }
}