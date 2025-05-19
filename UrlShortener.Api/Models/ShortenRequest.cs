namespace UrlShortener.Api.Models
{
    public class ShortenRequest
    {
        public string LongUrl { get; set; } = string.Empty;
        public int TTLMinutes { get; set; } = 1440; // default 1 day
    }
}



