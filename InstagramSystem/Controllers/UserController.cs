using InstagramSystem.DTOs;
using InstagramSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstagramSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetMyFriends()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value ?? "";
            var user = await _userService.GetFriendsById(int.Parse(userId));
            return Ok(user);
        }
        [HttpGet]
        [Route("Follow")]
        public async Task<IActionResult> Follow(int userId)
        {

            var result = await _userService.FollowUser(userId);
            return Ok(result);
        }
        [HttpGet]
        [Route("Accept")]
        public async Task<IActionResult> Accept(int userId)
        {

            var result = await _userService.ApproveRequestFollower(userId);
            return Ok(result);
        }
        #region Follows
        //todo
        #endregion
    }
}
