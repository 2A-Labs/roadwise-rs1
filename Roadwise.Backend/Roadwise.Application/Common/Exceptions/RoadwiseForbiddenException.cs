namespace Roadwise.Application.Common.Exceptions;

/// <summary>
/// The caller is authenticated but is not allowed to perform the action (e.g. a non-admin user).
/// Mapped to HTTP 403 with code "auth.forbidden".
/// </summary>
public sealed class RoadwiseForbiddenException : Exception
{
    public RoadwiseForbiddenException(string message) : base(message) { }
}
