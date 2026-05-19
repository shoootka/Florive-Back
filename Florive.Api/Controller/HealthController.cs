
using Florive.Domains.Models.Base;

public class OrderDto
{  public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}

public class ResponseMsg
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}

public interface IOrder
{
    ResponseMsg CreateOrderAction(OrderDto order);
    ResponseMsg UpdateOrderStatusAction(OrderDto order);
}

public class OrderAction
{
    public ResponseMsg ExecuteCreateOrderAction()
    {
        return new ResponseMsg
        {
            IsSuccess = true,
            Message = "Order created successfully",
        };
    }
    public ResponseMsg ExecuteUpdateOrderStatusAction()
    {
        return new ResponseMsg
        {
            IsSuccess = true,
            Message = "Order status updated successfully",
        };
    }
}

public class OrderFlow : OrderAction, IOrder
{
        public ResponseMsg CreateOrderAction(OrderDto order)
        {
            return ExecuteCreateOrderAction();
        }
     
        public ResponseMsg UpdateOrderStatusAction(OrderDto order)
        {
            return ExecuteUpdateOrderStatusAction();
        }
}