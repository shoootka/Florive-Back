using Florive.BusinessLogic.Core.SubscriptionOrders;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Functions.SubscriptionOrders
{
    public class SubscriptionOrderFlow : SubscriptionOrderAction, ISubscriptionOrder
    {
        public SubscriptionOrderFlow(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg GetAllOrdersAction()
        {
            return ExecuteGetAllOrdersAction();
        }
        public ResponseMsg GetOrdersByUserAction(int userId)
        {
            return ExecuteGetOrdersByUserAction(userId);
        }
        public ResponseMsg GetOrderByIdAction(int id)
        {
            return GetOrderDataByIdAction(id);
        }

        public ResponseMsg CreateOrderAction(SubscriptionOrderDTO order)
        {
            return ExecuteOrderCreateAction(order);
        }

        public ResponseMsg UpdateOrderAction(int id, SubscriptionOrderDTO order)
        {
            return ExecuteOrderUpdateAction(id, order);
        }

        public ResponseMsg DeleteOrderAction(int id)
        {
            return ExecuteOrderDeleteAction(id);
        }
    }
}