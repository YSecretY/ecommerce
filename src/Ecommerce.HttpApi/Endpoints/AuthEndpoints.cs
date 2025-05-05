using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Features.Users.Auth.Login;
using Ecommerce.Core.Features.Users.Auth.Register;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Users.Auth;
using Ecommerce.HttpApi.Contracts.Users.Auth.Login;
using Ecommerce.HttpApi.Contracts.Users.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/users/register", Register).WithOpenApi();
        group.MapPost("/users/login", Login).WithOpenApi();

        return group;
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] IUserRegisterUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.HandleAsync(request.ToCommand(), cancellationToken);

        return Results.Ok();
    }

    private static async Task<EndpointResult<IdentityTokenResponse>> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] IUserLoginCommandUseCase useCase,
        CancellationToken cancellationToken)
    {
        IdentityToken token = await useCase.HandleAsync(request.ToCommand(), cancellationToken);

        IdentityTokenResponse response = new(token);

        return new EndpointResult<IdentityTokenResponse>(response);
    }
}