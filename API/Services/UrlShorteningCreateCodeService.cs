using API.Data;
using API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class UrlShorteningCreateCodeService
{
    private readonly ApplicationDbContext _context;
    private readonly Random _random = new();

    public UrlShorteningCreateCodeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateUniqueCode()
    {
        var codeChars = new char[ShortLinkSettings.LENGTH];
        const int MAX_VALUE = 62;

        while (true)
        {
            for (int i = 0; i < ShortLinkSettings.LENGTH; i++)
            {
                var randomIndex = _random.Next(MAX_VALUE);
                codeChars[i] = ShortLinkSettings.ALPHABET[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _context.ShortenedUrls.AnyAsync(x => x.Code == code))
            {
                return code;
            }
        }
    }
}

