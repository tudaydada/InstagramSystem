using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Repositories;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InstagramSystem.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int Id);

        Task<User> register(RegisterDTO registerDTO);
        Task<User> login(LoginDTO loginDTO);
        UserClaim GetCurrentUser();
    }
    public class UserClaim {
        public string UserName { get; set;}
        public string UserEmail { get; set;}
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> login(LoginDTO loginDTO)
        {
            User user = new User();

            user = await userRepository.GetUserByUserName(loginDTO.UserName);
            var hashPass = GetMD5(loginDTO.Password);

            if (loginDTO.UserName == user.UserName && hashPass == user.Password)
            {
                return user;
            }
            return null;
        }

        public async Task<User> GetUserById(int Id)
        {
            return await userRepository.GetByIdAsync(Id);
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

            await userRepository.InsertAsync(user);
            userRepository.Save();

            var userResponse = await userRepository.GetUserByUserName(registerDTO.UserName);
            if (userResponse != null)
            {
                return userResponse;
            }
            return null;
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
        #endregion

    }
}
