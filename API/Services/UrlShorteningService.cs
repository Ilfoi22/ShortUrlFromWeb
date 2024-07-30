using API.Entities;
using API.Records;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class UrlShorteningService : IUrlShorteningService
{
    private readonly IUrlShorteningRepository _repository;

    public UrlShorteningService(IUrlShorteningRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ShortenedUrl>> GetAllShortenedUrlsAsync()
    {
        return await _repository.GetAllShortenedUrlsAsync();
    }

    public async Task<ShortenedUrl> GetShortUrlAsync(string code)
    {
        return await _repository.GetShortUrlAsync(code);
    }

    public async Task<ShortenedUrl> CreateShortUrlAsync(ShortenUrlRequest request)
    {
        return await _repository.CreateShortUrlAsync(request);
    }

    public async Task<ShortenedUrl> DeleteShortUrlAsync(Guid id)
    {
        return await _repository.DeleteShortUrlAsync(id);
    }
}
