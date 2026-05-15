using Florive.BusinessLogic.Core.Orders;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Functions.Orders
{
    public class OrderFunction : OrderAction, IOrder
    {
        public OrderFunction(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg GetAllOrdersAction()
        {
            return ExecuteGetAllOrdersAction();
        }

        public ResponseMsg CreateOrderAction(OrderDTO order)
        {
            return ExecuteCreateOrderAction(order);
        }

        public ResponseMsg UpdateOrderStatusAction(int id, string status)
        {
            return ExecuteUpdateOrderStatusAction(id, status);
        }

        public ResponseMsg DeleteOrderAction(int id)
        {
            return ExecuteDeleteOrderAction(id);
        }
        public ResponseMsg GetOrdersByUserAction(int userId)
        {
            return ExecuteGetOrdersByUserAction(userId);
        }
    }
}
