using Florive.Api.Attributes;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userService;

        public UserController(AppDbContext context)
        {
            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _userService = bl.GetUserActions();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAllUsersAction();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetUserByIdAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] UserDTO user)
        {
            var result = _userService.CreateUserAction(user);

            if (!result.IsSuccess)
                return BadRequest(result);

            var createdUser = (UserDTO)result.Data;
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDTO user)
        {
            var result = _userService.UpdateUserAction(id, user);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.DeleteUserAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("profile/{id}")]
        public IActionResult UpdateProfile(int id, [FromBody] UserDTO user)
        {
            var result = _userService.UpdateUserAction(id, user);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}