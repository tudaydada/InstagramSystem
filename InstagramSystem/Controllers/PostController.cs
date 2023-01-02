using InstagramSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InstagramSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        #region Search
        [HttpGet]
        [Route("i={id}")]
        public IActionResult GetByUsername(int id)
        {
            return Ok(id);
        }
        [HttpGet]
        [Route("search?s={search}")]
        public IActionResult Search(string search)
        {
            return Ok(search);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(AdvancePostSearchDto search)
        {
            return Ok(search);
        }
        [HttpGet]
        [Route("hastag?#{hastag}")]
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
        [Route("Privacy?type={type}")]
        public IActionResult UpdatePrivacy(string type)
        {
            return Ok(type);
        }
        [HttpPatch]
        [Route("i={id}")]
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
        #endregion
        #region Like
        //todo
        #endregion
    }
}
