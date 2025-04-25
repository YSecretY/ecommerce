using System.Security.Claims;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Core.Features.Auth.Shared.Internal;

internal interface IIdentityTokenGenerator
{
    public IdentityToken Generate(User user);

    public IdentityToken Generate(List<Claim> claims);

    public IdentityToken Refresh();
}