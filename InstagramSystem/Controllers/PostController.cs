using InstagramSystem.DTOs;
using InstagramSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstagramSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPostService _postService ;

        public PostController(IConfiguration configuration, IPostService postService)
        {
            _configuration = configuration;
            _postService = postService;
        }


        #region Search
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IActionResult GetByUsername(int id)
        {
            return Ok(id);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string search)
        {
            return Ok(search);
        }
        [HttpPost]
        [Route("search")]
        public IActionResult Search(AdvancePostSearchDto search)
        {
            return Ok(search);
        }
        [HttpGet]
        [Route("hastag")]
        public IActionResult SearchByHastag(string hastag)
        {
            return Ok(hastag);
        }
        #endregion
        #region Create And Update
        [HttpPost]
        [Route("NewPost")]
        public IActionResult NewPost(NewPostDto post)
        {
            return Ok(post);
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(UpdatePostDto post)
        {
            return Ok(post);
        }
        [HttpPatch]
        [Route("Privacy")]
        public IActionResult UpdatePrivacy(string type)
        {
            return Ok(type);
        }
        [HttpPatch]
        [Route("")]
        public IActionResult Delete(int id)
        {
            return Ok(id);
        }
        #endregion
        #region NewFeed
        [HttpGet]
        [Route("NewFeed")]
        public IActionResult GetNewFeed()
        {
            return Ok("NewFeed");
        }
        #endregion
        #region Comment
        //todo
        [HttpPost]
        [Route("Comment")]
        public IActionResult Like(int postId, string comment)
        {
            return Ok(new
            {
                postId,
                comment
            });
        }
        #endregion
        #region Like
        //todo
        [HttpGet]
        [Route("Like")]
        public IActionResult Like(int postId)
        {
            return Ok(postId);
        }
        #endregion
    }
}
