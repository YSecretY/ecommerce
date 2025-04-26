using Ecommerce.Core.Features.Users.Auth.Login;
using Ecommerce.Core.Features.Users.Auth.Register;
using Ecommerce.HttpApi.Contracts.Users.Auth;
using Ecommerce.HttpApi.Contracts.Users.Auth.Login;
using Ecommerce.HttpApi.Contracts.Users.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Auth;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(
    IUserRegisterUseCase registerUserUseCase,
    IUserLoginCommandUseCase loginUserUseCase
) : ControllerBase
{
    [HttpPost("/users/register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        UserRegisterCommand command = new(
            Email: request.Email,
            Password: request.Password,
            FirstName: request.FirstName,
            LastName: request.LastName
        );

        await registerUserUseCase.HandleAsync(command, cancellationToken);

        return Ok();
    }

    [HttpPost("/users/login")]
    public async Task<ActionResult<IdentityTokenResponse>> Login([FromBody] LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        UserLoginCommand command = new(Email: request.Email, Password: request.Password);

        return Ok(await loginUserUseCase.HandleAsync(command, cancellationToken));
    }
}