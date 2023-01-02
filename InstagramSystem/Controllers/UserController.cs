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
        [Route("Username")]
        public IActionResult GetByUsername(string username)
        {
            return Ok(username);
        }
        [HttpGet]
        [Route("Id")]
        public IActionResult GetById(int id)
        {
            return Ok(id);
        }
        [HttpGet]
        [Route("Search")]
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
        [Route("Friends")]
        public IActionResult GetFriends(int id, string username)
        {
            return Ok(new { id, username });
        }
        [HttpGet]
        [Route("MyFriends")]
        public IActionResult GetMyFriends()
        {

            var user = _userService.GetCurrentUser();
            return Ok(user);
        }
        #region Follows
        //todo
        #endregion
    }
}
