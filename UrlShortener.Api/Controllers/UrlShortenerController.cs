using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Models;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlShortenerService _service;

        public UrlShortenerController(IUrlShortenerService service)
        {
            _service = service;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> Shorten([FromBody] ShortenRequest request)
        {
            if (!Uri.TryCreate(request.LongUrl, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                return BadRequest("Invalid URL format. Must be absolute and use http or https.");
            }

            var result = await _service.ShortenUrlAsync(request.LongUrl, request.TTLMinutes);
            return Ok(new ShortenResponse { ShortUrl = result });
        }

        //[HttpGet("{code}")]
        //public async Task<IActionResult> RedirectToLongUrl(string code)
        //{
        //    var originalUrl = await _service.GetOriginalUrlAsync(code);
        //    if (originalUrl == null)
        //        return NotFound("Short link not found"); // 404 instead of 400
        //    return Redirect(originalUrl);
        //}

    }
}