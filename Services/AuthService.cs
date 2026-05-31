using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SkillFlow.Data;
using SkillFlow.DTOs.Users;
using SkillFlow.Models;
using SkillFlow.Services.Interfaces;

namespace SkillFlow.Services
{
    public class AuthService : IAuthService
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 100_000;

        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var email = dto.Email.Trim();
            var normalizedEmail = email.ToUpperInvariant();

            if (await _db.Users.AnyAsync(u => u.Email.ToUpper() == normalizedEmail))
            {
                return false;
            }

            var password = HashPassword(dto.Password);
            var user = new User
            {
                Email = email,
                Salt = password.Salt,
                PasswordHash = password.PasswordHash,
                Role = "User"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            await SignInAsync(user, isPersistent: false);

            return true;
        }

        public List<User> GetUsers()
        {
            return _db.Users.AsNoTracking().ToList();
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var email = loginDto.Email.Trim();
            var normalizedEmail = email.ToUpperInvariant();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == normalizedEmail);
            if (user == null)
            {
                return false;
            }

            if (!VerifyPassword(loginDto.Password, user.Salt, user.PasswordHash))
            {
                return false;
            }

            await SignInAsync(user, loginDto.RememberMe);
            return true;
        }

        public async Task LogoutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        public static (string PasswordHash, string Salt) HashPassword(string password)
        {
            var salt = GenerateSalt();
            return (ReturnHashPassword(password, salt), salt);
        }

        private static string ReturnHashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        private static bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            var passwordHash = ReturnHashPassword(password, salt);
            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(passwordHash),
                Convert.FromBase64String(hashedPassword));
        }

        private static string GenerateSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(SaltSize));
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.Email),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("No active HTTP context is available for sign-in.");

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }
    }
}
