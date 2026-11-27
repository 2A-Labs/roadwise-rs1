using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Roadwise.Application.Abstractions;

namespace Roadwise.Infrastructure.Common;

/// <summary>
/// Implementation of IAppCurrentUser that reads data from a JWT token.
/// </summary>
public sealed class AppCurrentUser(IHttpContextAccessor httpContextAccessor)
    : IAppCurrentUser
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    public int? UserId =>
        int.TryParse(_user?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
            ? id
            : null;

    public string? Email =>
        _user?.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated =>
        _user?.Identity?.IsAuthenticated ?? false;

    public bool IsAdmin =>
        _user?.FindFirstValue("is_admin")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;

    public bool IsInstructor =>
        _user?.FindFirstValue("is_instructor")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;

    public bool IsCandidate =>
        _user?.FindFirstValue("is_candidate")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;
}