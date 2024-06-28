using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyNZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(IdentityUser user, List<string> roles)
        {
            //create claim
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles) 
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            
            var cridentiial = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claim,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials : cridentiial
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
