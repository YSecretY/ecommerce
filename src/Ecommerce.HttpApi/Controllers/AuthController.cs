using Ecommerce.Core.Auth;
using Ecommerce.Core.Auth.Register;
using Ecommerce.HttpApi.Contracts.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(
    IAuthService authService
) : ControllerBase
{
    [HttpPost("/users/register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        RegisterUserCommand command = new(
            email: request.Email,
            password: request.Password,
            firstName: request.FirstName,
            lastName: request.LastName
        );

        await authService.RegisterAsync(command, cancellationToken);

        return Ok();
    }
}