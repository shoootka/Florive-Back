using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using Florive.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Florive.BusinessLogic.Core.Users
{
    public class UserAction
    {
        private readonly AppDbContext _context;

        public UserAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteGetAllUsersAction()
        {
            try
            {
                var users = _context.Users.ToList();

                var result = new List<UserDTO>();
                foreach (var user in users)
                {
                    result.Add(new UserDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        Phone = user.Phone,
                        Role = user.Role,
                        CreatedAt = user.CreatedAt,
                        IsActive = user.IsActive
                    });
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Успешно получены пользователи",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении пользователей: {ex.Message}"
                };
            }
        }

        protected ResponseMsg GetUserDataByIdAction(int id)
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

                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Пользователь с ID {id} не найден"
                    };
                }

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Пользователь найден",
                    Data = userDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении пользователя: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteUserCreateAction(UserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные пользователя не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Имя пользователя обязательно"
                    };
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Email обязателен"
                    };
                }

                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пользователь с таким именем уже существует"
                    };
                }

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пользователь с таким Email уже существует"
                    };
                }

                var newUser = new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    PasswordHash = string.Empty,  //пустой хэш (будет заполнен при регистрации)               
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Role = "User" 
                };

                var createdUserDTO = new UserDTO
                {
                    Id = newUser.Id,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    Phone = newUser.Phone,
                    Role = newUser.Role,
                    CreatedAt = newUser.CreatedAt,
                    IsActive = newUser.IsActive
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Пользователь создан успешно",
                    Data = createdUserDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании пользователя: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteUserUpdateAction(int id, UserDTO user)
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

                if (user == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные пользователя не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Имя пользователя обязательно"
                    };
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Email обязателен"
                    };
                }

                var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);

                if (existingUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Пользователь с ID {id} не найден"
                    };
                }

                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                // existingUser.IsActive = user.IsActive;
                existingUser.Role = user.Role;

                var updatedUserDTO = new UserDTO
                {
                    Id = existingUser.Id,
                    Username = existingUser.Username,
                    Email = existingUser.Email,
                    Phone = existingUser.Phone,
                    Role = existingUser.Role,
                    CreatedAt = existingUser.CreatedAt,
                    IsActive = existingUser.IsActive
                };

                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Пользователь обновлен успешно",
                    Data = updatedUserDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении пользователя: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteUserDeleteAction(int id)
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

                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Пользователь с ID {id} не найден"
                    };
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Пользователь удален успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении пользователя: {ex.Message}"
                };
            }
        }
    }
}