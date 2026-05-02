using Florive.BusinessLogic.Core.Users;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Functions.Users
{
    public class UserFlow : UserAction, IUser
    {
        public UserFlow(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg GetAllUsersAction()
        {
            return ExecuteGetAllUsersAction();
        }

        public ResponseMsg GetUserByIdAction(int id)
        {
            return GetUserDataByIdAction(id);
        }

        public ResponseMsg CreateUserAction(UserDTO user)
        {
            return ExecuteUserCreateAction(user);
        }

        public ResponseMsg UpdateUserAction(int id, UserDTO user)
        {
            return ExecuteUserUpdateAction(id, user);
        }

        public ResponseMsg DeleteUserAction(int id)
        {
            return ExecuteUserDeleteAction(id);
        }
    }
}