using API.Entities;
using API.Records;

namespace API.Repositories.Interfaces;

public interface IUrlShorteningRepository
{
    Task<List<ShortenedUrl>> GetAllShortenedUrlsAsync();
    Task<ShortenedUrl> GetShortUrlAsync(string code);
    Task<ShortenedUrl> CreateShortUrlAsync(ShortenUrlRequest request);
    Task<ShortenedUrl> DeleteShortUrlAsync(Guid id);
}
