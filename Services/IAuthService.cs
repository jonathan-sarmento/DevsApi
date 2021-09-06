using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DevsApi.Services
{
    public interface IAuthService
    {
        IdentityUser GetUser(IdentityUser identityUser);
        Task<SignInResult> ValidateUser(IdentityUser identityUser);
        Task<IdentityResult> Create(IdentityUser identityUser);
        string GetUserRole(IdentityUser identityUser);
        string GenerateToken(IdentityUser identityUser);
    }
}