using Florive.Domains.Models;
using Florive.Domains.Models.Base;

using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Interface
{
    public interface ICart
    {
        ResponseMsg GetCartByUserAction(int userId);
        ResponseMsg AddToCartAction(CartItemDTO dto);
        ResponseMsg UpdateCartItemAction(int id, CartItemDTO dto);
        ResponseMsg DeleteCartItemAction(int id);
        ResponseMsg ClearCartAction(int userId);
    }
}
