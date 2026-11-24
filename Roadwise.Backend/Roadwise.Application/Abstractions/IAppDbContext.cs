using Roadwise.Domain.Entities.Finance;

namespace Roadwise.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
    DbSet<RoadwiseUserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }
    DbSet<PasswordResetTokenEntity> PasswordResetTokens { get; }

    DbSet<CurrencyEntity> Currencies { get; }
    DbSet<ExchangeRateEntity> ExchangeRates { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
