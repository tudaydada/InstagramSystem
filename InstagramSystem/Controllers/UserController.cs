using InstagramSystem.DTOs;
using InstagramSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InstagramSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("Username={username}")]
        public IActionResult GetByUsername(string username)
        {
            return Ok(username);
        }
        [HttpGet]
        [Route("Id={id}")]
        public IActionResult GetById(int id)
        {
            return Ok(id);
        }
        [HttpGet]
        [Route("Search?s={search}")]
        public IActionResult Search(string search)
        {
            return Ok(search);
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(UserUpdateDto user)
        {
            return Ok(user);
        }
        [HttpGet]
        [Route("Friends?username={username}")]
        public IActionResult GetFriends(string username)
        {
            return Ok(username);
        }
        [HttpGet]
        [Route("Friends?id={id}")]
        public IActionResult GetFriends(int id)
        {
            return Ok(id);
        }
        [HttpGet]
        [Route("Friends")]
        public IActionResult GetFriends()
        {

            var user = _userService.GetCurrentUser();
            return Ok(user);
        }
        #region Follows
        //todo
        #endregion
    }
}
