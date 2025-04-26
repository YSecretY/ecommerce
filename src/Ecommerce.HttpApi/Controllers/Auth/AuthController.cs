using Ecommerce.Core.Features.Auth;
using Ecommerce.Core.Features.Auth.Login;
using Ecommerce.Core.Features.Auth.Register;
using Ecommerce.HttpApi.Contracts.Auth;
using Ecommerce.HttpApi.Contracts.Auth.Login;
using Ecommerce.HttpApi.Contracts.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Auth;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(
    IAuthService authService
) : ControllerBase
{
    [HttpPost("/users/register")]
    public async Task<ActionResult<IdentityTokenResponse>> Register([FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        RegisterUserCommand command = new(
            Email: request.Email,
            Password: request.Password,
            FirstName: request.FirstName,
            LastName: request.LastName
        );

        return Ok(await authService.RegisterAsync(command, cancellationToken));
    }

    [HttpPost("/users/login")]
    public async Task<ActionResult<IdentityTokenResponse>> Login([FromBody] LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        LoginUserCommand command = new(request.Email, request.Password);

        return Ok(await authService.LoginAsync(command, cancellationToken));
    }
}