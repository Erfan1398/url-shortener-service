using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Models;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Controllers
{
    [ApiController]
[Route("{code}")]
public class RedirectController : ControllerBase
{
    private readonly IUrlShortenerService _service;

    public RedirectController(IUrlShortenerService service)
    {
        _service = service;
    }


        [HttpGet]
        public async Task<IActionResult> RedirectToLongUrl(string code)
        {
            var originalUrl = await _service.GetOriginalUrlAsync(code);
            if (originalUrl == null)
                return NotFound("Short link not found"); // 404 instead of 400
            return Redirect(originalUrl);
        }


     

 }
}