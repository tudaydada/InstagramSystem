using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Repositories;

namespace InstagramSystem.Services
{
    public interface IPostCommentService
    {
        Task<PostComment> create(PostComment postComment);

        void update(PostComment postComment);

        void delete(PostComment postComment);
    }

    public class PostCommentService : IPostCommentService
    {
        private readonly IPostCommentRepository postCommentRepository;

        public PostCommentService(IPostCommentRepository postCommentRepository)
        {
            this.postCommentRepository = postCommentRepository;
        }

        public async Task<PostComment> create(PostComment postComment)
        {
            PostComment NewPostComment = new PostComment();

            NewPostComment.Message = postComment.Message;
            //NewPostComment.CreateAt = DateTime.Now;
            NewPostComment.UserId = postComment.UserId;
            NewPostComment.PostId = postComment.PostId;

            await postCommentRepository.InsertAsync(NewPostComment);
            postCommentRepository.Save();

            var postCommentResponse = await postCommentRepository.GetByIdAsync(NewPostComment.PostId);
            if(postCommentResponse == null)
            {
                return null;
            }
            return postCommentResponse;
        }

        public void delete(PostComment postComment)
        {
            postComment.IsDelete = true;

            postCommentRepository.Update(postComment);

            postCommentRepository.Save();

        }

        public void update(PostComment postComment)
        {
            postCommentRepository.Update(postComment);

            postCommentRepository.Save();
        }
    }
}
