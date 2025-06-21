using Microsoft.AspNetCore.Identity;

namespace EG.Walks.Application.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
