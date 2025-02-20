using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Contracts.Repositories;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Services.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    
    private readonly IConfiguration _configuration;
    
    private readonly UserManager<User> _userManager;

    public AuthService(IUserRepository userRepository,
        IConfiguration configuration,
        UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<AuthResponse> GoogleLoginAsync(string accessToken, string email, string name)
    {
        // 5. Validate Google token
        var payload = await GoogleJsonWebSignature.ValidateAsync(accessToken);
    
        // 6. Find or create user in database
        var user = await _userManager.FindByEmailAsync(email) ?? new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = name,
        };

        // 7. Generate JWT token
        var token = JwtSecurityToken(user);

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            Username = user.UserName
        };

        JwtSecurityToken JwtSecurityToken(User user1)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user1.Email),
                new Claim(ClaimTypes.Name, user1.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return jwtSecurityToken;
        }
    }
    
    public async Task<int> RegisterAsync(User user)
    {
        var result = await _userRepository.CreateAsync(user);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value.Id;
    }

    public async Task<User> LoginAsync(string email, string password)
    {
        var result = await _userRepository.GetUserByEmailPassword(email, password);
        result.OnFailure(() => throw new ArgumentException(result.Error));
        
        return result.Value;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var result = await _userRepository.GetAllAsync();
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        var result = await _userRepository.GetByIdAsync(userId);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }
}
