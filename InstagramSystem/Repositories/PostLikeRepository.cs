using InstagramSystem.Data;
using InstagramSystem.Entities;

namespace InstagramSystem.Repositories
{

    public interface IPostLikeRepository : IBaseRepository<PostLike>
    {

    }
    public class PostLikeRepository : BaseRepository<PostLike>, IPostLikeRepository
    {
        public PostLikeRepository(DataContext context) : base(context)
        {
        }

    }
}
