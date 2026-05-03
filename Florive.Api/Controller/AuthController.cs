using Florive.Api.Attributes;
using Florive.BusinessLogic;
using Florive.DataAccess;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BusinessLogic.BusinessLogic _businessLogic;

        public AuthController(AppDbContext context)
        {
            _businessLogic = new BusinessLogic.BusinessLogic(context);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO registerData)
        {
            var result = _businessLogic.GetAuthFlow().Register(registerData);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginData)
        {
            var result = _businessLogic.GetAuthFlow().Login(loginData);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            // новая сессия
            var loginResponse = (LoginResponseDTO)result.Data;
            var userId = loginResponse.Id;

            var sessionResult = _businessLogic.GetSessionActions().CreateSessionAction(userId);

            if (!sessionResult.IsSuccess)
            {
                return BadRequest(sessionResult);
            }

            var sessionKey = sessionResult.Data.ToString();

            // установка куки
            Response.Cookies.Append("X-KEY", sessionKey, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });

            return Ok(result);
        }

        [RequireAuth]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // получаем куки
            var sessionKey = Request.Cookies["X-KEY"];

            if (!string.IsNullOrEmpty(sessionKey))
            {
                _businessLogic.GetSessionActions().DeleteSessionAction(sessionKey);
            }

            // удаляем куки
            Response.Cookies.Delete("X-KEY");

            return Ok(new { IsSuccess = true, Message = "Выход выполнен" });
        }

        [RequireAuth]
        [HttpGet("validate")]
        public IActionResult ValidateSession()
        {
            var sessionKey = Request.Cookies["X-KEY"];

            if (string.IsNullOrEmpty(sessionKey))
            {
                return Unauthorized(new { IsSuccess = false, Message = "Сессия не найдена" });
            }

            var result = _businessLogic.GetSessionActions().ValidateSessionAction(sessionKey);

            return result.IsSuccess ? Ok(result) : Unauthorized(result);
        }
    }
}