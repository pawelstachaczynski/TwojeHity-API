using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TwojeHity.Models.DTOs;
using TwojeHity.Services;

namespace TwojeHity.Controllers
{

     [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {

            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserDto> GetUser([FromRoute] int id)
        {
            _userService.GetById(id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return NotFound();
        }



    }
}