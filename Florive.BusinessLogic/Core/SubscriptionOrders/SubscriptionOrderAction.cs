using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Core.SubscriptionOrders
{
    public class SubscriptionOrderAction
    {
        private readonly AppDbContext _context;

        public SubscriptionOrderAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteGetAllOrdersAction()
        {
            try
            {
                var orders = _context.SubscriptionOrders.ToList();

                var result = new List<SubscriptionOrderDTO>();
                foreach (var order in orders)
                {
                    result.Add(new SubscriptionOrderDTO
                    {
                        Id = order.Id,
                        SubscriptionPlanId = order.SubscriptionPlanId,
                        FirstFlowerId = order.FirstFlowerId,
                        Name = order.Name,
                        Phone = order.Phone,
                        Email = order.Email,
                        Address = order.Address,
                        Frequency = order.Frequency,
                        FirstDeliveryDate = order.FirstDeliveryDate,
                        Comment = order.Comment,
                        Status = order.Status
                    });
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Успешно получены заказы подписок",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении заказов подписок: {ex.Message}"
                };
            }
        }

        protected ResponseMsg GetOrderDataByIdAction(int id)
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

                var order = _context.SubscriptionOrders.FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Заказ подписки с ID {id} не найден"
                    };
                }

                var orderDTO = new SubscriptionOrderDTO
                {
                    Id = order.Id,
                    SubscriptionPlanId = order.SubscriptionPlanId,
                    FirstFlowerId = order.FirstFlowerId,
                    Name = order.Name,
                    Phone = order.Phone,
                    Email = order.Email,
                    Address = order.Address,
                    Frequency = order.Frequency,
                    FirstDeliveryDate = order.FirstDeliveryDate,
                    Comment = order.Comment,
                    Status = order.Status
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказ подписки найден",
                    Data = orderDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении заказа подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteOrderCreateAction(SubscriptionOrderDTO order)
        {
            try
            {
                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные заказа подписки не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(order.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Имя обязательно"
                    };
                }

                if (string.IsNullOrWhiteSpace(order.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Email обязателен"
                    };
                }

                var newOrder = new SubscriptionOrder
                {
                    UserId = order.UserId,
                    SubscriptionPlanId = order.SubscriptionPlanId,
                    FirstFlowerId = order.FirstFlowerId,
                    Name = order.Name,
                    Phone = order.Phone,
                    Email = order.Email,
                    Address = order.Address,
                    Frequency = order.Frequency,
                    FirstDeliveryDate = order.FirstDeliveryDate,
                    Comment = order.Comment,
                    Status = "New"
                };

                _context.SubscriptionOrders.Add(newOrder);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказ подписки создан успешно",
                    Data = newOrder
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании заказа подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteOrderUpdateAction(int id, SubscriptionOrderDTO order)
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

                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные заказа подписки не переданы"
                    };
                }

                var existingOrder = _context.SubscriptionOrders.FirstOrDefault(o => o.Id == id);

                if (existingOrder == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Заказ подписки с ID {id} не найден"
                    };
                }

                existingOrder.Name = order.Name;
                existingOrder.Phone = order.Phone;
                existingOrder.Email = order.Email;
                existingOrder.Address = order.Address;
                existingOrder.Frequency = order.Frequency;
                existingOrder.FirstDeliveryDate = order.FirstDeliveryDate;
                existingOrder.Comment = order.Comment;
                existingOrder.Status = order.Status;

                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказ подписки обновлен успешно",
                    Data = existingOrder
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении заказа подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteOrderDeleteAction(int id)
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

                var order = _context.SubscriptionOrders.FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Заказ подписки с ID {id} не найден"
                    };
                }

                _context.SubscriptionOrders.Remove(order);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Заказ подписки удален успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении заказа подписки: {ex.Message}"
                };
            }
        }
    }
}