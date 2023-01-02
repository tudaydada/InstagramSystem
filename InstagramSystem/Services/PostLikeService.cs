using InstagramSystem.Entities;
using InstagramSystem.Repositories;

namespace InstagramSystem.Services
{
    public interface IPostLikeService
    {
        Task<PostLike> create(PostLike postLike);

        void update(PostLike postLike);

        void delete(PostLike postLike);
    }

    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository postLikeRepository;

        public PostLikeService(IPostLikeRepository postLikeRepository)
        {
            this.postLikeRepository = postLikeRepository;
        }

        public async Task<PostLike> create(PostLike postLike)
        {
            PostLike newPostLike = new PostLike();

            //newPostLike.CreateAt = DateTime.Now;
            newPostLike.UserId= postLike.UserId;
            newPostLike.PostId= postLike.PostId;

            await postLikeRepository.InsertAsync(newPostLike);
            postLikeRepository.Save();

            var postLikeResponse = await postLikeRepository.GetByIdAsync(newPostLike.Id);
            if(postLikeResponse == null)
            {
                return null;
            }
            return postLikeResponse ;  
        }

        public void delete(PostLike postLike)
        {
            postLike.IsDelete = true;

            postLikeRepository.Update(postLike);

            postLikeRepository.Save();
        }

        public void update(PostLike postLike)
        {
            postLikeRepository.Update(postLike);

            postLikeRepository.Save();
        }
    }
}
