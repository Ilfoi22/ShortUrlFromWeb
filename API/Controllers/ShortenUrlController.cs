using API.Entities;
using API.Records;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ShortenUrlController : ControllerBase
{
    private readonly IUrlShorteningService _urlService;

    public ShortenUrlController(IUrlShorteningService urlService)
    {
        _urlService = urlService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<ShortenedUrl>>> GetAllShortenedUrlsAsync()
    {
        var result = await _urlService.GetAllShortenedUrlsAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{code}")]
    public async Task<ActionResult<ShortenedUrl>> GetShortUrlAsync(string code)
    {
        var result = await _urlService.GetShortUrlAsync(code);

        if (result == null)
        {
            return NotFound();
        }

        return Redirect(result.LongUrl);
    }

    [HttpPost]
    public async Task<ActionResult<ShortenedUrl>> CreateShortUrlAsync([FromBody] ShortenUrlRequest request)
    {
        var result = await _urlService.CreateShortUrlAsync(request);

        if (result == null)
        {
            return NoContent();
        }

        result.ShortUrl = $"{Request.Scheme}://{Request.Host}/api/ShortenUrl/{result.Code}";

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ShortenedUrl>> DeleteShortUrlAsync(Guid id)
    {
        var result = await _urlService.DeleteShortUrlAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
