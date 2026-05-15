using Florive.BusinessLogic.Core.Carts;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Functions.Carts
{
    public class CartFunction : CartAction, ICart
    {
        public CartFunction(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg GetCartByUserAction(int userId)
        {
            return ExecuteGetCartByUserAction(userId);
        }

        public ResponseMsg AddToCartAction(CartItemDTO dto)
        {
            return ExecuteAddToCartAction(dto);
        }

        public ResponseMsg UpdateCartItemAction(int id, CartItemDTO dto)
        {
            return ExecuteUpdateCartItemAction(id, dto);
        }

        public ResponseMsg DeleteCartItemAction(int id)
        {
            return ExecuteDeleteCartItemAction(id);
        }

        public ResponseMsg ClearCartAction(int userId)
        {
            return ExecuteClearCartAction(userId);
        }
    }
}