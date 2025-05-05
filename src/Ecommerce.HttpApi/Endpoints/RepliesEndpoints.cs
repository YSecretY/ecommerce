using Ecommerce.Core.Features.Replies.Create;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Users.Reviews.ReviewReplies.Create;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class RepliesEndpoints
{
    public static RouteGroupBuilder MapRepliesEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateReviewReply).WithOpenApi();

        return group;
    }

    private static async Task<EndpointResult<Guid>> CreateReviewReply(
        [FromBody] UserCreateReviewReplyRequest request,
        [FromServices] IUserCreateReviewReplyUseCase userCase,
        CancellationToken cancellationToken
    ) => new(await userCase.HandleAsync(request.ToCommand(), cancellationToken));
}