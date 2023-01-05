using InstagramSystem.Data;
using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Repositories;
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
    }
    public interface IUserService
    {
        Task<User> GetUserById(int Id);

        Task<User> register(RegisterDTO registerDTO);
        Task<User> login(LoginDTO loginDTO);
        ResponseDTO ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        UserClaim GetCurrentUser();
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor,DataContext context,IEmailService emailService)
        {
            this.userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _emailService = emailService;
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
            return await userRepository.GetUserById(Id);
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

        public ResponseDTO ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == forgotPasswordDto.UserName && x.Email == forgotPasswordDto.Email);
            if (user == null)
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
                var password = user.UserName + (DateTime.Now.ToString());
                var emailForm = new EmailFormDto();
                emailForm.Subject = "Reset Password";
                emailForm.Body = "UserName:"+user.UserName+"</br>Password:"+ password;
                emailForm.To= user.Email;
                try
                {
                    _emailService.SendEmail(emailForm);
                    user.Password = GetMD5(password);
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return new ResponseDTO
                    {
                        Success = true,
                        Message = "Please Check Your Email to get password"
                    };
                }
                catch(Exception ex)
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
                return user;
            }
            return user;
        }
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

    }
}
