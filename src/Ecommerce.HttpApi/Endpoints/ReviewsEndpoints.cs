using Ecommerce.Core.Abstractions.Models.Reviews;
using Ecommerce.Core.Features.Reviews.Create;
using Ecommerce.Core.Features.Reviews.DeleteById;
using Ecommerce.Core.Features.Reviews.GetList;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Reviews;
using Ecommerce.HttpApi.Contracts.Reviews.Create;
using Ecommerce.HttpApi.Contracts.Reviews.GetList;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class ReviewsEndpoints
{
    public static RouteGroupBuilder MapReviewsEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateReview).WithOpenApi();
        group.MapGet("/", GetReviewsList).WithOpenApi();
        group.MapDelete("/{id:guid}", DeleteReview).WithOpenApi();

        return group;
    }

    private static async Task<EndpointResult<Guid>> CreateReview(
        [FromBody] UserCreateReviewRequest request,
        [FromServices] IUserCreateReviewUseCase useCase,
        CancellationToken cancellationToken)
    {
        Guid reviewId = await useCase.HandleAsync(request.ToCommand(), cancellationToken);

        return new EndpointResult<Guid>(reviewId);
    }

    private static async Task<EndpointResult<ProductsReviewsGetListResponse>> GetReviewsList(
        [AsParameters] PaginationRequest request,
        [FromServices] IUserGetReviewsListUseCase useCase,
        CancellationToken cancellationToken)
    {
        PaginatedEnumerable<ProductReviewDto> reviews =
            await useCase.HandleAsync(request.ToPaginationQuery(), cancellationToken);

        ProductsReviewsGetListResponse response = new(reviews.Map(r => new ProductReviewResponse(r)));

        return new EndpointResult<ProductsReviewsGetListResponse>(response);
    }

    private static async Task<IResult> DeleteReview(
        Guid id,
        [FromServices] IUserDeleteReviewByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.HandleAsync(id, cancellationToken);

        return Results.Ok();
    }
}