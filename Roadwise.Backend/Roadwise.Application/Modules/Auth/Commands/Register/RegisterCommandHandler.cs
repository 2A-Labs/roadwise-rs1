using Roadwise.Application.Modules.Auth.Common;

namespace Roadwise.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandHandler(
    IAppDbContext ctx,
    IJwtTokenService jwt,
    IPasswordHasher<RoadwiseUserEntity> hasher)
    : IRequestHandler<RegisterCommand, RegisterCommandDto>
{
    public async Task<RegisterCommandDto> Handle(RegisterCommand request, CancellationToken ct)
    {
        // Required fields, lengths and the password rule are checked by RegisterCommandValidator.
        // The unique email needs the database, so it is checked here (409, see ValidationBehavior).
        // Case-insensitive, like login; soft-deleted users are skipped by the global query filter
        // (the unique index on Email also ignores them and stays as the last guard).
        var email = request.Email.Trim().ToLowerInvariant();
        if (await ctx.Users.AnyAsync(u => u.Email.ToLower() == email, ct))
            throw new RoadwiseConflictException("Email je već registrovan.");

        // A new account never has a role: roles cannot be chosen during registration.
        var user = new RoadwiseUserEntity
        {
            Firstname = request.Firstname.Trim(),
            Lastname = request.Lastname.Trim(),
            Email = email,
            Address = request.Address.TrimToNull(),
            City = request.City.TrimToNull(),
            IsAdmin = false,
            IsManager = false,
            IsEmployee = false,
            IsEnabled = true
        };
        user.PasswordHash = hasher.HashPassword(user, request.Password);

        ctx.Users.Add(user);

        // Saved first: the access token contains the user's Id, which the database assigns.
        await ctx.SaveChangesAsync(ct);

        // The user is logged in right away, like after login
        var tokens = ctx.StartSession(jwt, user);
        await ctx.SaveChangesAsync(ct);

        return new RegisterCommandDto
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshTokenRaw,
            AccessTokenExpiresAtUtc = tokens.AccessTokenExpiresAtUtc,
            RefreshTokenExpiresAtUtc = tokens.RefreshTokenExpiresAtUtc
        };
    }
}
