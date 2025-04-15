using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.IServices
{
    public interface IJWTTokenGenerator
    {
        Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user);
    }
}
