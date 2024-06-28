using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyNZWalks.API.Models.DTOs;
using MyNZWalks.API.Repositories;

namespace MyNZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST: api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            // Tạo một đối tượng IdentityUser với thông tin đăng ký từ request
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            // Thực hiện việc tạo người dùng mới trong hệ thống Identity
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    // Nếu có quyền được chỉ định, thêm người dùng vào các quyền đó
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered successfully. Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }
        //POST: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if(user != null) 
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if(checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null) 
                    {
                        var jwtToken = tokenRepository.CreateToken(user, roles.ToList());

                        var reponse = new LoginReponseDTO
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(reponse);
                    }
                }
            }
            return BadRequest("Username or password iscorrect");
            
        }
    }
}