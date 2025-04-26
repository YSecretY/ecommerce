using System.Security.Claims;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Infrastructure.Auth.Abstractions;

public interface IIdentityTokenGenerator
{
    public IdentityToken Generate(User user);

    public IdentityToken Generate(List<Claim> claims);

    public IdentityToken Refresh();
}