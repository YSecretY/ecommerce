using Ecommerce.Core.Features.Users.Reviews;
using Ecommerce.Core.Features.Users.Reviews.Create;
using Ecommerce.Core.Features.Users.Reviews.DeleteById;
using Ecommerce.Core.Features.Users.Reviews.GetList;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Users.GetList;
using Ecommerce.HttpApi.Contracts.Users.Reviews;
using Ecommerce.HttpApi.Contracts.Users.Reviews.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Users;

[ApiController]
[Authorize]
[Route("/api/v1/users/reviews")]
public class UsersProductsReviewsController(
    IUserCreateReviewUseCase userCreateReviewUseCase,
    IUserDeleteReviewByIdUseCase userDeleteReviewByIdUseCase,
    IUserGetReviewsListUseCase userGetReviewsListUseCase
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

    [HttpGet]
    public async Task<ActionResult<EndpointResult<ProductsReviewsGetListResponse>>> GetReviewsList(
        [FromQuery] PaginationRequest request, CancellationToken cancellationToken)
    {
        PaginatedEnumerable<ProductReviewDto> reviews = await userGetReviewsListUseCase.HandleAsync(
            new PaginationQuery(request.PageSize, request.PageNumber), cancellationToken
        );

        ProductsReviewsGetListResponse response = new(reviews.Map(r => new ProductReviewResponse(r)));

        return new EndpointResult<ProductsReviewsGetListResponse>(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReview([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await userDeleteReviewByIdUseCase.HandleAsync(id, cancellationToken);

        return Ok();
    }
}