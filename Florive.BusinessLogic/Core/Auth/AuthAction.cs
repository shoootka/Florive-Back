using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Core.Auth
{
    public class AuthAction
    {
        private readonly AppDbContext _context;

        public AuthAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteRegisterAction(RegisterDTO registerData)
        {
            try
            {
                if (registerData == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные регистрации не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(registerData.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Имя пользователя обязательно"
                    };
                }

                if (string.IsNullOrWhiteSpace(registerData.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Email обязателен"
                    };
                }

                if (string.IsNullOrWhiteSpace(registerData.Password) || registerData.Password.Length < 6)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пароль должен быть минимум 6 символов"
                    };
                }

                //уникальность email
                if (_context.Users.Any(u => u.Email == registerData.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пользователь с таким email уже существует"
                    };
                }

                //уникальность юзернейма
                if (_context.Users.Any(u => u.Username == registerData.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пользователь с таким именем уже существует"
                    };
                }

                //хэширование пароля , по-моему не очень , но пусть будет
                string passwordHash = HashPassword(registerData.Password);

                var newUser = new User
                {
                    Username = registerData.Username,
                    Email = registerData.Email,
                    PasswordHash = passwordHash,
                    Phone = string.Empty,
                    Role = "User",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                var response = new LoginResponseDTO
                {
                    Id = newUser.Id,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    Role = newUser.Role
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Регистрация успешна",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при регистрации: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteLoginAction(LoginDTO loginData)
        {
            try
            {
                if (loginData == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные входа не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(loginData.Email))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Email обязателен"
                    };
                }

                if (string.IsNullOrWhiteSpace(loginData.Password))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пароль обязателен"
                    };
                }

                var user = _context.Users.FirstOrDefault(u => u.Email == loginData.Email);

                if (user == null || !VerifyPassword(loginData.Password, user.PasswordHash))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Неверный email или пароль"
                    };
                }

                if (!user.IsActive)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Пользователь деактивирован"
                    };
                }

                var response = new LoginResponseDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Вход успешен",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при входе: {ex.Message}"
                };
            }
        }

        // тоже что то с хэшированием .....
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}