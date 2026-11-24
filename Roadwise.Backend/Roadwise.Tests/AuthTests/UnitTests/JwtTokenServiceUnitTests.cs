using System.IdentityModel.Tokens.Jwt;
using Roadwise.Domain.Entities.Identity;
using Roadwise.Infrastructure.Common;
using Roadwise.Shared.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Time.Testing;

namespace Roadwise.Tests.AuthTests.UnitTests;

/// <summary>
/// Claims in the access token that the Angular client reads (core/services/auth/jwt-payload.dto.ts).
/// </summary>
public class JwtTokenServiceUnitTests
{
    [Fact]
    public void IssueTokens_AccessToken_ContainsClaimsReadByFrontend()
    {
        var options = Options.Create(new JwtOptions
        {
            Issuer = "Roadwise.Api",
            Audience = "Roadwise.Spa",
            Key = "UNIT_TESTS_ONLY_KEY_0123456789_ABCDEFGHIJ"
        });
        var service = new JwtTokenService(options, new FakeTimeProvider(DateTimeOffset.UtcNow));
        var user = new RoadwiseUserEntity { Id = 7, Email = "kandidat@roadwise.local", IsAdmin = false, TokenVersion = 3 };

        var token = new JwtSecurityTokenHandler().ReadJwtToken(service.IssueTokens(user).AccessToken);
        string Claim(string type) => token.Claims.Single(c => c.Type == type).Value;

        Assert.Equal("7", Claim(JwtRegisteredClaimNames.Sub));
        Assert.Equal("kandidat@roadwise.local", Claim(JwtRegisteredClaimNames.Email)); // payload.email
        Assert.Equal("false", Claim("is_admin"));
        Assert.Equal("3", Claim("ver"));
    }
}
