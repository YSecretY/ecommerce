using Ecommerce.Core.Users.Reviews.Create;
using Ecommerce.Core.Users.Reviews.DeleteById;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Reviews.Users.Create;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Users;

[ApiController]
[Route("/api/v1/users/reviews")]
public class UsersProductsReviewsController(
    IUserCreateReviewUseCase userCreateReviewUseCase,
    IUserDeleteReviewByIdUseCase userDeleteReviewByIdUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EndpointResult<Guid>>> CreateReview([FromBody] UserCreateReviewRequest request,
        CancellationToken cancellationToken)
    {
        UserCreateReviewCommand command = new(
            productId: request.ProductId,
            text: request.Text
        );

        return new EndpointResult<Guid>(await userCreateReviewUseCase.HandleAsync(command, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReview([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await userDeleteReviewByIdUseCase.HandleAsync(id, cancellationToken);

        return Ok();
    }
}