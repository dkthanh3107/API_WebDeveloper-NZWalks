using Microsoft.AspNetCore.Identity;

namespace MyNZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles); 
    }
}
