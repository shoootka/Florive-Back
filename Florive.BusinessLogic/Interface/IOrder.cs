using Florive.Domains.Models;
using Florive.Domains.Models.Base;

using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Interface
{
    public interface IOrder
    {
        ResponseMsg GetAllOrdersAction();
        ResponseMsg GetOrdersByUserAction(int userId);
        ResponseMsg CreateOrderAction(OrderDTO dto);
        ResponseMsg UpdateOrderStatusAction(int id, string status);
        ResponseMsg DeleteOrderAction(int id);
    }
}
