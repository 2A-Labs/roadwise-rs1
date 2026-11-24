namespace Roadwise.Application.Abstractions.Caching;

/// <summary>
/// Simple "cache-aside" helper over <c>IDistributedCache</c>.
/// <para>
/// The starter registers it but does not use it anywhere. Without configuration it runs on an in-memory cache;
/// with a non-empty <c>ConnectionStrings:Redis</c> it runs on Redis, and handlers do not change.
/// </para>
/// <para>
/// Example — cache a list that rarely changes (e.g. license categories) for 5 minutes:
/// <code>
/// var dto = await cache.GetOrCreateAsync("license-categories:all", token => LoadFromDatabaseAsync(token), TimeSpan.FromMinutes(5), ct);
/// </code>
/// A handler that changes the cached data must then remove or version the key, so the next request reads fresh data.
/// </para>
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Returns the cached value for <paramref name="key"/>; if there is none, calls <paramref name="factory"/>,
    /// stores its result for <paramref name="ttl"/> and returns it. Values are stored as JSON,
    /// so <typeparamref name="T"/> must be a serializable DTO (not an EF entity).
    /// </summary>
    Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan ttl,
        CancellationToken cancellationToken = default) where T : class;
}
