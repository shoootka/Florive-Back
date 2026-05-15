using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;

namespace Florive.BusinessLogic.Core.Orders
{
    public class OrderAction
    {
        private readonly AppDbContext _context;

        public OrderAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteGetAllOrdersAction()
        {
            try
            {
                var orders = _context.Orders.ToList();

                var result = new List<OrderDTO>();
                foreach (var order in orders)
                {
                    result.Add(MapToDTO(order));
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Успешно получены заказы",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении заказов: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteGetOrdersByUserAction(int userId)
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

                var orders = _context.Orders
                    .Where(o => o.UserId == userId)
                    .ToList();

                var result = new List<OrderDTO>();
                foreach (var order in orders)
                {
                    result.Add(MapToDTO(order));
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказы пользователя успешно получены",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении заказов пользователя: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteCreateOrderAction(OrderDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные заказа не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Имя получателя обязательно"
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Address))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Адрес доставки обязателен"
                    };
                }

                // берем товары из корзины пользователя
                var cartItems = _context.CartItems
                    .Where(c => c.UserId == dto.UserId)
                    .ToList();

                if (cartItems.Count == 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Корзина пуста — нечего оформлять"
                    };
                }

                // считаем TotalPrice из актуальных цен продуктов
                decimal totalPrice = 0;
                var orderItemsData = new List<(CartItem cartItem, Product product)>();

                foreach (var cartItem in cartItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);

                    if (product == null)
                    {
                        return new ResponseMsg
                        {
                            IsSuccess = false,
                            Message = $"Продукт с ID {cartItem.ProductId} не найден"
                        };
                    }

                    totalPrice += product.Price * cartItem.Quantity;
                    orderItemsData.Add((cartItem, product));
                }

                // создаем заказ
                var newOrder = new Order
                {
                    UserId = dto.UserId,
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    Address = dto.Address,
                    TotalPrice = totalPrice,
                    Status = "New",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                // создаем OrderItems с фиксацией цены на момент заказа
                foreach (var (cartItem, product) in orderItemsData)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = newOrder.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = product.Price
                    };

                    _context.OrderItems.Add(orderItem);
                }

                // очищаем корзину после оформления
                _context.CartItems.RemoveRange(cartItems);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказ успешно оформлен",
                    Data = newOrder
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании заказа: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteUpdateOrderStatusAction(int id, string status)
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

                if (string.IsNullOrWhiteSpace(status))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Статус заказа обязателен"
                    };
                }

                var order = _context.Orders.FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Заказ с ID {id} не найден"
                    };
                }

                order.Status = status;
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Статус заказа обновлен",
                    Data = order
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении статуса заказа: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteDeleteOrderAction(int id)
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

                var order = _context.Orders.FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Заказ с ID {id} не найден"
                    };
                }

                var orderItems = _context.OrderItems.Where(i => i.OrderId == id).ToList();
                _context.OrderItems.RemoveRange(orderItems);

                _context.Orders.Remove(order);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказ удален успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении заказа: {ex.Message}"
                };
            }
        }

        private OrderDTO MapToDTO(Order order)
        {
            var items = _context.OrderItems
                .Where(i => i.OrderId == order.Id)
                .ToList()
                .Select(i =>
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == i.ProductId);
                    return new OrderItemDTO
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        ProductName = product?.Name ?? string.Empty,
                        Quantity = i.Quantity,
                        Price = i.Price
                    };
                })
                .ToList();

            return new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                Name = order.Name,
                Phone = order.Phone,
                Email = order.Email,
                Address = order.Address,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                Items = items
            };
        }
    }
}