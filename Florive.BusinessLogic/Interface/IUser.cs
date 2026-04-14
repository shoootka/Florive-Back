using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Interface
{
    public interface IUser
    {
        ResponseMsg GetAllUsersAction();
        ResponseMsg GetUserByIdAction(int id);
        ResponseMsg CreateUserAction(UserDTO user);
        ResponseMsg UpdateUserAction(int id, UserDTO user);
        ResponseMsg DeleteUserAction(int id);
    }
}
