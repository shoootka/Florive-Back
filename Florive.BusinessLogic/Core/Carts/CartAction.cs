using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Core.Carts
{
    public class CartAction
    {
        private readonly AppDbContext _context;

        public CartAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteGetCartByUserAction(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID пользователя должен быть больше 0"
                    };
                }

                var items = _context.CartItems
                    .Where(c => c.UserId == userId)
                    .Select(c => new CartItemDTO
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity
                    })
                    .ToList();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Корзина успешно получена",
                    Data = items
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении корзины: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteAddToCartAction(CartItemDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные товара не переданы"
                    };
                }

                if (dto.Quantity <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Количество должно быть больше 0"
                    };
                }

                var product = _context.Products.FirstOrDefault(p => p.Id == dto.ProductId);

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {dto.ProductId} не найден"
                    };
                }

                // если товар уже есть в корзине — увеличиваем quantity
                var existingItem = _context.CartItems
                    .FirstOrDefault(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += dto.Quantity;
                    _context.SaveChanges();

                    return new ResponseMsg
                    {
                        IsSuccess = true,
                        Message = "Количество товара в корзине обновлено",
                        Data = existingItem
                    };
                }

                var newItem = new CartItem
                {
                    UserId = dto.UserId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                _context.CartItems.Add(newItem);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Товар добавлен в корзину",
                    Data = newItem
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при добавлении товара в корзину: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteUpdateCartItemAction(int id, CartItemDTO dto)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID должен быть больше 0"
                    };
                }

                if (dto == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные товара не переданы"
                    };
                }

                if (dto.Quantity <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Количество должно быть больше 0"
                    };
                }

                var item = _context.CartItems.FirstOrDefault(c => c.Id == id);

                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Позиция с ID {id} не найдена в корзине"
                    };
                }

                item.Quantity = dto.Quantity;
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Количество товара обновлено",
                    Data = item
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении позиции в корзине: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteDeleteCartItemAction(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID должен быть больше 0"
                    };
                }

                var item = _context.CartItems.FirstOrDefault(c => c.Id == id);

                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Позиция с ID {id} не найдена в корзине"
                    };
                }

                _context.CartItems.Remove(item);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Товар удален из корзины"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении товара из корзины: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteClearCartAction(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID пользователя должен быть больше 0"
                    };
                }

                var items = _context.CartItems
                    .Where(c => c.UserId == userId)
                    .ToList();

                if (items.Count > 0)
                {
                    _context.CartItems.RemoveRange(items);
                    _context.SaveChanges();
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Корзина очищена"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при очистке корзины: {ex.Message}"
                };
            }
        }
    }
}