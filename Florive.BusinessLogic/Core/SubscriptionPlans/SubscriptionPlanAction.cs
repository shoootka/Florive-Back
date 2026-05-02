using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Core.SubscriptionPlans
{
    public class SubscriptionPlanAction
    {
        private readonly AppDbContext _context;

        public SubscriptionPlanAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteGetAllPlansAction()
        {
            try
            {
                var plans = _context.SubscriptionPlans.ToList();

                var result = new List<SubscriptionPlanDTO>();
                foreach (var plan in plans)
                {
                    result.Add(new SubscriptionPlanDTO
                    {
                        Id = plan.Id,
                        Name = plan.Name,
                        Price = plan.Price,
                        DeliveriesCount = plan.DeliveriesCount,
                        Description = plan.Description
                    });
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Успешно получены планы подписки",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении планов подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg GetPlanDataByIdAction(int id)
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

                var plan = _context.SubscriptionPlans.FirstOrDefault(p => p.Id == id);

                if (plan == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"План подписки с ID {id} не найден"
                    };
                }

                var planDTO = new SubscriptionPlanDTO
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Price = plan.Price,
                    DeliveriesCount = plan.DeliveriesCount,
                    Description = plan.Description
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "План подписки найден",
                    Data = planDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении плана подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecutePlanCreateAction(SubscriptionPlanDTO plan)
        {
            try
            {
                if (plan == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные плана подписки не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(plan.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Название плана обязательно"
                    };
                }

                if (plan.Price <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Цена должна быть больше 0"
                    };
                }

                var newPlan = new SubscriptionPlan
                {
                    Name = plan.Name,
                    Price = plan.Price,
                    DeliveriesCount = plan.DeliveriesCount,
                    Description = plan.Description
                };

                _context.SubscriptionPlans.Add(newPlan);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "План подписки создан успешно",
                    Data = newPlan
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании плана подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecutePlanUpdateAction(int id, SubscriptionPlanDTO plan)
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

                if (plan == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные плана подписки не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(plan.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Название плана обязательно"
                    };
                }

                var existingPlan = _context.SubscriptionPlans.FirstOrDefault(p => p.Id == id);

                if (existingPlan == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"План подписки с ID {id} не найден"
                    };
                }

                existingPlan.Name = plan.Name;
                existingPlan.Price = plan.Price;
                existingPlan.DeliveriesCount = plan.DeliveriesCount;
                existingPlan.Description = plan.Description;

                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "План подписки обновлен успешно",
                    Data = existingPlan
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении плана подписки: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecutePlanDeleteAction(int id)
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

                var plan = _context.SubscriptionPlans.FirstOrDefault(p => p.Id == id);

                if (plan == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"План подписки с ID {id} не найден"
                    };
                }

                _context.SubscriptionPlans.Remove(plan);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "План подписки удален успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении плана подписки: {ex.Message}"
                };
            }
        }
    }
}
