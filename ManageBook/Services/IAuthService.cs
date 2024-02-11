using ManageBook.Models;

namespace ManageBook.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateTokenString(LoginUser user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> Login(LoginUser user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> RegisterUser(RegisterUser user);
    }
}
