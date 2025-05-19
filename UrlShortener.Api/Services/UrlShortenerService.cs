using StackExchange.Redis;
using UrlShortener.Infrastructure.Redis;

namespace UrlShortener.Api.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly RedisUrlRepository _repo;
        private readonly string _baseUrl;

        
        public UrlShortenerService(IConnectionMultiplexer redis , IConfiguration config)
        {
            _repo = new RedisUrlRepository(redis);
            _baseUrl = config["App:BaseUrl"] ?? "http://localhost:5000";

        }

        public async Task<string> ShortenUrlAsync(string longUrl, int ttlMinutes)
        {
            var code = GenerateCode(longUrl);
            var exists = await _repo.GetAsync(code);

            if (!exists.HasValue)
            {
                await _repo.SetAsync(code, longUrl, TimeSpan.FromMinutes(ttlMinutes));
            }
            var shortUrl = $"{_baseUrl}/{code}";

            return shortUrl;
        }

        public async Task<string?> GetOriginalUrlAsync(string code)
        {
            var result = await _repo.GetAsync(code);
            return result.HasValue ? result.ToString() : null;
        }

        private string GenerateCode(string input)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input + Guid.NewGuid()));
            return Convert.ToBase64String(hash).Replace("/", "_").Replace("+", "-").Substring(0, 8);
        }
	}
}