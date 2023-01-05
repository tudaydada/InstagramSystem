using InstagramSystem.Data;
using InstagramSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstagramSystem.Repositories
{
    public interface IPostCommentRepository : IBaseRepository<PostComment>
    {
        
    }

    public class PostCommentRepository : BaseRepository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(DataContext context) : base(context)
        {

        }

    }
}
