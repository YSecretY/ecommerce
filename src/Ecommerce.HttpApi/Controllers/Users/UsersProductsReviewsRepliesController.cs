using Ecommerce.Core.Features.Users.Replies.Create;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Users.Reviews.ReviewReplies.Create;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Users;

[ApiController]
[Route("/api/v1/users/reviews/replies")]
public class UsersProductsReviewsRepliesController(
    IUserCreateReviewReplyUseCase createReviewReplyUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EndpointResult<Guid>>> CreateReviewReply([FromBody] UserCreateReviewReplyRequest request,
        CancellationToken cancellationToken)
    {
        UserCreateReviewReplyCommand command = new(request.ReviewId, request.Text);

        return Ok(new EndpointResult<Guid>(await createReviewReplyUseCase.HandleAsync(command, cancellationToken)));
    }
}