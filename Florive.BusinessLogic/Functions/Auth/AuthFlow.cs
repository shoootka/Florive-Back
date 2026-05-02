using Florive.BusinessLogic.Core.Auth;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Functions.Auth
{
    public class AuthFlow : AuthAction, IAuth
    {
        public AuthFlow(AppDbContext context) : base(context) { }

        public ResponseMsg Register(RegisterDTO registerData)
        {
            return ExecuteRegisterAction(registerData);
        }

        public ResponseMsg Login(LoginDTO loginData)
        {
            return ExecuteLoginAction(loginData);
        }
    }
}