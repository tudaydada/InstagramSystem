using InstagramSystem.Data;
using InstagramSystem.Entities;

namespace InstagramSystem.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {

    }

    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }
    }
}
