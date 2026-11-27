namespace Roadwise.Shared.Constants;

/// <summary>
/// Names of the authorization policies registered in Roadwise.API (see <c>AddAuthorization</c>).
/// Use them with <c>[Authorize(Policy = PolicyNames.Admin)]</c> instead of repeating string literals.
/// </summary>
public static class PolicyNames
{
    /// <summary>
    /// Only users whose access token contains the claim <c>is_admin = "true"</c>.
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// Only users whose access token contains the claim <c>is_instructor = "true"</c>.
    /// </summary>
    public const string Instructor = "Instructor";

    /// <summary>
    /// Only users whose access token contains the claim <c>is_candidate = "true"</c>.
    /// </summary>
    public const string Candidate = "Candidate";
}
