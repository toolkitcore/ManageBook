using ManageBook.Models;

namespace ManageBook.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(RegisterUser user);
    }
}
