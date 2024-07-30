using System.Security.Claims;
using API.Data;
using API.Entities;
using API.Records;
using API.Repositories.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UrlShorteningRepository : IUrlShorteningRepository
{
    private readonly UrlShorteningCreateCodeService _urlShorteningService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UrlShorteningRepository(UrlShorteningCreateCodeService urlShorteningService,
        ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _urlShorteningService = urlShorteningService;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<ShortenedUrl>> GetAllShortenedUrlsAsync()
    {
        return await _dbContext.ShortenedUrls.ToListAsync();
    }

    public async Task<ShortenedUrl> GetShortUrlAsync(string code)
    {
        var shortenedUrl = await _dbContext
            .ShortenedUrls
            .SingleOrDefaultAsync(x => x.Code == code);

        if (shortenedUrl == null)
        {
            return new ShortenedUrl { };
        }

        return shortenedUrl;
    }

    public async Task<ShortenedUrl> CreateShortUrlAsync(ShortenUrlRequest request)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
        {
            throw new Exception("Invalid URL");
        }

        var code = await _urlShorteningService.GenerateUniqueCode();

        var httpRequest = _httpContextAccessor.HttpContext?.Request;

        var userId = GetUserId();

        var shortenedUrl = new ShortenedUrl
        {
            Id = Guid.NewGuid(),
            LongUrl = request.Url,
            Code = code,
            ShortUrl = $"{httpRequest?.Scheme}://{httpRequest?.Host}/api/shortenurl/{code}",
            CreatedDate = DateTime.UtcNow,
            CreatedBy = userId
        };

        _dbContext.ShortenedUrls.Add(shortenedUrl);

        await _dbContext.SaveChangesAsync();

        return shortenedUrl;
    }

    public async Task<ShortenedUrl> DeleteShortUrlAsync(Guid id)
    {
        var shortenedUrl = await _dbContext.ShortenedUrls.FindAsync(id);

        if (shortenedUrl == null)
        {
            return new ShortenedUrl { };
        }

        var userId = GetUserId();

        if (!IsAdmin() && shortenedUrl.CreatedBy != userId)
        {
            return null;
        }

        _dbContext.ShortenedUrls.Remove(shortenedUrl);

        await _dbContext.SaveChangesAsync();

        return shortenedUrl;
    }

    private bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext.User.IsInRole("admin");
    }

    private int GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new Exception("User ID claim not found.");
        }

        Console.WriteLine($"User ID claim value: {userIdClaim}");

        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new FormatException($"The user ID '{userIdClaim}' is not in a correct format.");
        }

        return userId;
    }

}
