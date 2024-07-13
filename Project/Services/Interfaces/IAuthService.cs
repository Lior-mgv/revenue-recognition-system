using FinancesProject.DTO;

namespace FinancesProject.Services;

public interface IAuthService
{
    Task RegisterUser(RegistrationForm model);
    Task<(string accessToken, string refreshToken)> LoginUser(LoginForm model);
    Task<(string accessToken, string newRefreshToken)> RefreshToken(string refreshToken);
}