using Ecommerce.Core.Users.Reviews.Create;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Reviews.Users.Create;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Users;

[ApiController]
[Route("/api/v1/users/reviews")]
public class UsersProductsReviewsController(
    IUserCreateReviewUseCase userCreateReviewUseCase
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
}