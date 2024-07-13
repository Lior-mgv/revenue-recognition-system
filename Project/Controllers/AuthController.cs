using FinancesProject.DTO;
using FinancesProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancesProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegistrationForm form)
    {
        await _service.RegisterUser(form);
        
        return Ok();
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginForm form)
    {
        var (accessToken, refreshToken) = await _service.LoginUser(form);
        
        return Ok(new {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var (accessToken, newRefreshToken) =  await _service.RefreshToken(refreshToken);

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        });
    }
}