using StackExchange.Redis;

namespace UrlShortener.Infrastructure.Redis
{
    public class RedisUrlRepository
    {
        private readonly IDatabase _db;

        public RedisUrlRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public Task<bool> SetAsync(string code, string url, TimeSpan ttl)
        {
            return _db.StringSetAsync(code, url, ttl);
        }

        public Task<RedisValue> GetAsync(string code)
        {
            return _db.StringGetAsync(code);
        }
    }
}