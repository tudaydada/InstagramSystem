using InstagramSystem.Commons;
using InstagramSystem.DTOs;
using InstagramSystem.Entities;
using InstagramSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InstagramSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return Ok(new ResponseDTO
                {
                    Success = false,
                    Message = "Register Failed!"
                });
            }
            else
            {
                var user = userService.register(registerDTO);
                if (user == null)
                {
                    return Ok(new ResponseDTO
                    {
                        Success = false,
                        Message = "Register Failed!",
                        Data = registerDTO
                    });
                }
                else
                {
                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "Register successfully!",
                        Data = user
                    });
                }
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return Ok(new ResponseDTO
                {
                    Success = false,
                    Message = "Login Failed"
                });
            }
            else
            {
                var user = await userService.login(loginDTO);
                if (user == null)
                {
                    return Ok(new ResponseDTO
                    {
                        Success = false,
                        Message = "Login Failed"
                    });
                }
                else
                {
                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "Login Successfully",
                        Data = GenerateToken(user)
                    });
                }
            }

        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return Ok("ForgotPassword");

        }

        #region [Private method Generate token]
        /// <summary>
        /// Generate Token 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration.GetSection("SecretKey").Value);

            var role = user.RoleId == ((int)EUserRole.Admin) ? EUserRole.Admin.ToString() : user.RoleId == ((int)EUserRole.User) ? EUserRole.User.ToString() : "unknow";

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    //new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("UserName", user.UserName),
                    new Claim("UserId", user.Id.ToString()),

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
        #endregion

    }
}
