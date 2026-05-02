using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Interface
{
    public interface IAuth
    {
        ResponseMsg Register(RegisterDTO registerData);
        ResponseMsg Login(LoginDTO loginData);
    }
}