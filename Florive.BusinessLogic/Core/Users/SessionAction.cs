using Florive.Domains.Entities;
using Florive.Domains.Models.Base;
using Florive.DataAccess;
using System;
using System.Linq;

namespace Florive.BusinessLogic.Core.Users
{
    public class SessionAction
    {
        private readonly AppDbContext _context;

        public SessionAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteCreateSessionAction(int userId)
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

                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Пользователь с ID {userId} не найден"
                    };
                }

                var sessionKey = Guid.NewGuid().ToString();

                var session = new UserSession
                {
                    UserId = userId,
                    SessionKey = sessionKey,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60)
                };

                _context.UserSessions.Add(session);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Сессия создана успешно",
                    Data = sessionKey
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании сессии: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteValidateSessionAction(string sessionKey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sessionKey))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Ключ сессии не передан"
                    };
                }

                var session = _context.UserSessions
                    .FirstOrDefault(s => s.SessionKey == sessionKey);

                if (session == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Сессия не найдена"
                    };
                }

                if (session.ExpiresAt < DateTime.UtcNow)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Сессия истекла"
                    };
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Сессия валидна",
                    Data = session.UserId
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при проверке сессии: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteDeleteSessionAction(string sessionKey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sessionKey))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Ключ сессии не передан"
                    };
                }

                var session = _context.UserSessions
                    .FirstOrDefault(s => s.SessionKey == sessionKey);

                if (session == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Сессия не найдена"
                    };
                }

                _context.UserSessions.Remove(session);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Сессия удалена успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении сессии: {ex.Message}"
                };
            }
        }
    }
}