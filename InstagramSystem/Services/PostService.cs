using InstagramSystem.Commons;
using InstagramSystem.Data;
using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstagramSystem.Services
{
    public interface IPostService
    {
        // Post
        Task<List<PostDto>> GetAll();
        Task<List<PostDto>> GetNewFeeds();
        Task<List<PostDto>> GetNewFeeds(int userId);
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
        private readonly DataContext _context;
        public PostService(IPostRepository postRepository, IPostCommentRepository postCommentRepository, IPostLikeRepository postLikeRepository, IUserService userService,DataContext context)
        {
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _postLikeRepository = postLikeRepository;
            _userService = userService;
            _context = context;
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

        public async Task<List<PostDto>> GetAll()
        {
            var result = new List<PostDto>();
            var posts = await _context.Posts.Select(x => new PostDto
            {
                Id = x.Id,
                PostLikes= x.PostLikes,
                Privacy= x.Privacy,
                PostComments= x.PostComments,
                Type= x.Type,
                Content= x.Content,
                CreateAt= DateTime.Now,
                hagtag= x.hagtag,
                Users= x.Users,
            }).ToListAsync();
            result.AddRange(posts);
            return result;
        }

        public async Task<List<PostDto>> GetNewFeeds()
        {
            var user =  _userService.GetCurrentUser();
            var idUserFollower =  _context.UserFollowers.Where(x => x.UserId == int.Parse(user.UserId)).Select(x=>x.UserFollowerId).ToList();
            var post = await  _context.Posts.Where(x=> idUserFollower.Contains(x.UserId)).OrderByDescending(x=>x.CreateAt).Select(x => new PostDto
            {
                Id = x.Id,
                PostLikes = x.PostLikes,
                Privacy = x.Privacy,
                Type = x.Type,
                Content = x.Content,
                CreateAt = DateTime.Now,
                hagtag = x.hagtag,
                Users = new User { UserName = x.Users.UserName,Id = x.Id}
            }).ToListAsync();
            return post;
        }

        public Task<List<PostDto>> GetNewFeeds(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
