namespace UrlShortener.Api.Services
{
    public interface IUrlShortenerService
    {
        Task<string> ShortenUrlAsync(string longUrl, int ttlMinutes);
        Task<string?> GetOriginalUrlAsync(string code);
    }
}