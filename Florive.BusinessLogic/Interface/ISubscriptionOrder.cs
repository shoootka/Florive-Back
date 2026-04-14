using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Interface
{
    public interface ISubscriptionOrder
    {
        ResponseMsg GetAllOrdersAction();
        ResponseMsg GetOrderByIdAction(int id);
        ResponseMsg CreateOrderAction(SubscriptionOrderDTO order);
        ResponseMsg UpdateOrderAction(int id, SubscriptionOrderDTO order);
        ResponseMsg DeleteOrderAction(int id);
    }
}
