using InstagramSystem.Commons;
using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InstagramSystem.Services
{
    public interface IPostService
    {
        // Post
        Task<PostDto> GetPost(int id);
        Task<NewPostDto> CreatePost(NewPostDto post);
        Task<PostDto> Post(PostDto id);
        Task<IActionResult> UpdatePost(PostDto post);
        Task<IActionResult> DeletePost(int id);


        // PostComment 
        Task<PostComment> CreatePostComment(PostComment postComment);
        void UpdatePostComment(PostComment postComment);
        void DeletePostComment(PostComment postComment);

        // PostLike
        Task<PostLike> CreatePostLike(PostLike postLike);
        void UpdatePostLike(PostLike postLike);
        void DeletePostLike(PostLike postLike);

    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IUserService _userService;

        public PostService(IPostRepository postRepository, IPostCommentRepository postCommentRepository, IPostLikeRepository postLikeRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _postLikeRepository = postLikeRepository;
            _userService = userService;
        }

        #region [Post Service]
        public Task<IActionResult> DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostDto> GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostDto> Post(PostDto id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdatePost(PostDto post)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region [Post Comment Service]
        public async Task<PostComment> CreatePostComment(PostComment postComment)
        {
            PostComment NewPostComment = new PostComment();

            NewPostComment.Message = postComment.Message;
            //NewPostComment.CreateAt = DateTime.Now;
            NewPostComment.UserId = postComment.UserId;
            NewPostComment.PostId = postComment.PostId;

            await _postCommentRepository.InsertAsync(NewPostComment);
            _postCommentRepository.Save();

            var postCommentResponse = await _postCommentRepository.GetByIdAsync(NewPostComment.PostId);
            if (postCommentResponse == null)
            {
                return null;
            }
            return postCommentResponse;
        }

        public void DeletePostComment(PostComment postComment)
        {
            postComment.IsDelete = true;

            _postCommentRepository.Update(postComment);

            _postCommentRepository.Save();

        }

        public void UpdatePostComment(PostComment postComment)
        {
            _postCommentRepository.Update(postComment);

            _postCommentRepository.Save();
        }
        #endregion


        #region [Post Like Service]
        public async Task<PostLike> CreatePostLike(PostLike postLike)
        {
            PostLike newPostLike = new PostLike();

            //newPostLike.CreateAt = DateTime.Now;
            newPostLike.UserId = postLike.UserId;
            newPostLike.PostId = postLike.PostId;

            await _postLikeRepository.InsertAsync(newPostLike);
            _postLikeRepository.Save();

            var postLikeResponse = await _postLikeRepository.GetByIdAsync(newPostLike.Id);
            if (postLikeResponse == null)
            {
                return null;
            }
            return postLikeResponse;
        }

        public void DeletePostLike(PostLike postLike)
        {
            postLike.IsDelete = true;

            _postLikeRepository.Update(postLike);

            _postLikeRepository.Save();
        }

        public void UpdatePostLike(PostLike postLike)
        {
            _postLikeRepository.Update(postLike);

            _postLikeRepository.Save();
        }

        public async Task<NewPostDto> CreatePost(NewPostDto post)
        {

            try
            {
                var user = _userService.GetCurrentUser();
                var newPost = new Post();
                newPost.Content = post.Content ?? "";
                newPost.Type = post.Type ?? EPostType.None.ToString();
                newPost.hagtag = post.hagtag ?? "";
                newPost.UserId = int.Parse(user.UserId);
                newPost.Privacy = int.Parse(user.Privacy);
                newPost.CreateAt = DateTime.Now;
                newPost.Privacy = int.Parse(user.UserId);
                newPost.IsDelete = false;
                var result = await _postRepository.InsertAsync(newPost);
                _postRepository.Save();
                return post;
            }
            catch
            (Exception ex)
            {
                return null;
            }
        }
        #endregion


    }
}
