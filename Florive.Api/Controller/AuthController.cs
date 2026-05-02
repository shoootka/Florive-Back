using Microsoft.AspNetCore.Mvc;
using Florive.BusinessLogic;    
using Florive.Domains.Models;
using Florive.DataAccess;

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

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}