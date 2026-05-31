namespace SkillFlow.Services.Interfaces
{
    using SkillFlow.DTOs.Users;
    using SkillFlow.Models;

    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto model);

        Task<bool> LoginAsync(LoginDto model);

        Task LogoutAsync();

        List<User> GetUsers();
    }
}
