using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Interface
{
    public interface ISession
    {
        ResponseMsg CreateSessionAction(int userId);
        ResponseMsg ValidateSessionAction(string sessionKey);
        ResponseMsg DeleteSessionAction(string sessionKey);
    }
}