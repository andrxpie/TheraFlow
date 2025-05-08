using BLL.Entities;
using System.Security.Claims;

namespace BLL.Interfaces
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaims(User user);
        string CreateToken(IEnumerable<Claim> claims);
        string CreateRefreshToken();
        IEnumerable<Claim> GetClaimsFromExpiredToken(string token);
        DateTime GetLastValidRefreshTokenDate();
    }
}
