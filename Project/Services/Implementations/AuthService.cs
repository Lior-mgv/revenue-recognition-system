using System.Security.Claims;
using FinancesProject.Context;
using FinancesProject.DTO;
using FinancesProject.Exceptions;
using FinancesProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinancesProject.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;

    private readonly SecurityHelpers _securityHelpers;

    public AuthService(AppDbContext dbContext, SecurityHelpers securityHelpers)
    {
        _dbContext = dbContext;
        _securityHelpers = securityHelpers;
    }

    public async Task RegisterUser(RegistrationForm model)
    {
        if (await _dbContext.Users.AnyAsync(e => e.Login == model.Login))
        {
            throw new BadRequestException("This username is already taken");
        }
        
        var (hashedPassword, salt) = _securityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser
        {
            Login = model.Login,
            Password = hashedPassword,
            Salt = salt,
            Role = "User"
        };

        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<(string accessToken, string refreshToken)> LoginUser(LoginForm model)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Login == model.Login);
        if (user == null) throw new BadRequestException("Login is incorrect.");
        
        var hashedPassword = _securityHelpers.GetHashedPasswordWithSalt(model.Password, user.Salt);
        if (hashedPassword != user.Password) throw new BadRequestException("Password is incorrect.");

        var (accessToken, refreshToken) = GenerateTokensForUser(user);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(30);
        await _dbContext.SaveChangesAsync();

        return (accessToken, refreshToken);
    }

    public async Task<(string accessToken, string newRefreshToken)> RefreshToken(string refreshToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token.");
        }
        
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        var (accessToken, newRefreshToken) = GenerateTokensForUser(user);
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(30);
        await _dbContext.SaveChangesAsync();

        return (accessToken, newRefreshToken);
    }

    private (string, string) GenerateTokensForUser(AppUser user)
    {
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.IdAppUser)),
            new Claim(ClaimTypes.UserData, user.Login),
            new Claim(ClaimTypes.Role, user.Role)
        };
        
        var accessToken = _securityHelpers.GenerateAccessToken(userClaims);
        var refreshToken = _securityHelpers.GenerateRefreshToken();
        return (accessToken, refreshToken);
    }
}