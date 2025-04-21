using System.Security.Claims;
using Ecommerce.Domain;

namespace Ecommerce.Core.Auth.Shared.Internal;

internal interface IIdentityTokenGenerator
{
    public IdentityToken Generate(User user);

    public IdentityToken Generate(List<Claim> claims);

    public IdentityToken Refresh();
}