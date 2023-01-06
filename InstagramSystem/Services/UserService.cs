using InstagramSystem.Commons;
using InstagramSystem.Data;
using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InstagramSystem.Services
{
    public class UserClaim
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Privacy { get; set; }
    }
    public interface IUserService
    {
        Task<User> GetUserById(int Id);
        Task<UserFollower> FollowUser(int userId);
        Task<UserFollower> ApproveRequestFollower(int userId);
        Task<List<string>> GetFriendsById(int userId);
        Task<User> register(RegisterDTO registerDTO);
        Task<User> login(LoginDTO loginDTO);
        Task<ResponseDTO> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        UserClaim GetCurrentUser();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, DataContext context, IEmailService emailService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _emailService = emailService;
        }

        public async Task<User> login(LoginDTO loginDTO)
        {
            User user = new User();

            user = await _userRepository.GetUserByUserName(loginDTO.UserName);
            var hashPass = GetMD5(loginDTO.Password);

            if (loginDTO.UserName == user.UserName && hashPass == user.Password)
            {
                return user;
            }
            return null;
        }

        public async Task<User> GetUserById(int Id)
        {
            return await _userRepository.GetByIdAsync(Id);
        }

        public async Task<User> register(RegisterDTO registerDTO)
        {
            User user = new User();

            user.UserName = registerDTO.UserName;
            user.Password = GetMD5(registerDTO.Password);
            user.FullName = registerDTO.FullName;
            user.Birthday = registerDTO.Birthday;
            user.Email = registerDTO.Email;
            user.Address = registerDTO.Address;
            user.Sex = registerDTO.Sex;
            user.Phone = registerDTO.Phone;
            user.ImageURL = registerDTO.ImageURL;
            user.RoleId = registerDTO.RoleId;

            await _userRepository.InsertAsync(user);
            _userRepository.Save();

            var userResponse = await _userRepository.GetUserByUserName(registerDTO.UserName);
            if (userResponse != null)
            {
                return userResponse;
            }
            return null;
        }

        public async Task<ResponseDTO> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userRepository.GetUserByUserName(forgotPasswordDto.UserName);
            if (user == null || !user.Email.Equals(forgotPasswordDto.Email))
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Not found",
                    Code = 404
                };
            }
            else
            {
                var password = RandomPassword();
                var emailForm = new EmailFormDto();
                emailForm.Subject = "Reset Password";
                emailForm.Body = "UserName:[" + user.UserName + "]_Password:[" + password + "]";
                emailForm.To = user.Email;
                try
                {
                    _emailService.SendEmail(emailForm);
                    user.Password = GetMD5(password);
                    _userRepository.Update(user);
                    _userRepository.Save();
                    return new ResponseDTO
                    {
                        Success = true,
                        Message = "Please Check Your Email to get password"
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseDTO
                    {
                        Success = false,
                        Message = ex.Message,
                        Code = 500
                    };
                }
            }
        }


        public UserClaim GetCurrentUser()
        {
            var user = new UserClaim();
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                user.FullName = httpContext.User.FindFirstValue(ClaimTypes.Name) ?? "";
                user.UserId = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value ?? "";
                user.Role = httpContext.User.FindFirstValue(ClaimTypes.Role) ?? "";
                user.UserName = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserName"))?.Value ?? "";
                user.Privacy = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Privacy"))?.Value ?? "";
                return user;
            }
            return user;
        }

        #region [Private method GetMD5]
        private string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
        private string RandomPassword()
        {
            string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string NUMERIC_CHARACTERS = "0123456789";
            string SPECIAL_CHARACTERS = @"!#$%&*@\";
            int PASSWORD_LENGTH_MIN = 8;
            int PASSWORD_LENGTH_MAX = 12;

            Random random = new Random();
            string valid = "";

            valid += (LOWERCASE_CHARACTERS[random.Next(0, LOWERCASE_CHARACTERS.Length)]);
            valid += (UPPERCASE_CHARACTERS[random.Next(0, UPPERCASE_CHARACTERS.Length)]);
            valid += (NUMERIC_CHARACTERS[random.Next(0, NUMERIC_CHARACTERS.Length)]);
            valid += (SPECIAL_CHARACTERS[random.Next(0, SPECIAL_CHARACTERS.Length)]);
            valid += (LOWERCASE_CHARACTERS[random.Next(0, LOWERCASE_CHARACTERS.Length)]);
            valid += (UPPERCASE_CHARACTERS[random.Next(0, UPPERCASE_CHARACTERS.Length)]);
            valid += (NUMERIC_CHARACTERS[random.Next(0, NUMERIC_CHARACTERS.Length)]);
            valid += (SPECIAL_CHARACTERS[random.Next(0, SPECIAL_CHARACTERS.Length)]);



            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < PASSWORD_LENGTH_MIN--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);

            }
            return res.ToString();
        }

        public async Task<UserFollower> FollowUser(int userId)
        {
            var user = GetCurrentUser();
            var userFollower = new UserFollower();
            userFollower.UserFollowerId = int.Parse(user.UserId);
            userFollower.UserId = userId;
            userFollower.CreateAt = DateTime.Now;
            userFollower.Status = ((int)EUserFollowerStatus.Pending);
            _context.UserFollowers.Add(userFollower);
            _context.SaveChangesAsync();
            return userFollower;

        }

        public async Task<List<string>> GetFriendsById(int userId)
        {
            var idFollower = _context.UserFollowers.Where(x => x.UserId == userId && x.Status== ((int)EUserFollowerStatus.Approve)).Select(x=>x.UserFollowerId).ToList();
            var result = await _context.Users.Where(x => idFollower.Contains(x.Id)).Select(x=>x.UserName).ToListAsync();
            return result;
        }

        public async Task<UserFollower> ApproveRequestFollower(int userId)
        {
            var user = GetCurrentUser();
            var follow = await _context.UserFollowers.Where(x=>x.UserId==int.Parse(user.UserId)&&x.UserFollowerId==userId).FirstOrDefaultAsync();
            if (follow == null)
                return null;
            follow.Status = ((int)EUserFollowerStatus.Approve);
            _context.UserFollowers.Update(follow);
            _context.SaveChangesAsync();
            var result = new UserFollower();
            result.Users = follow.Users;
            return result;
        }
        #endregion

    }
}
