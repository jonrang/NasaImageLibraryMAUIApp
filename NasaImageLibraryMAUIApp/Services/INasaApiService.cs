using NasaImageLibraryMAUIApp.Models;

namespace NasaImageLibraryMAUIApp.Services
{
    public interface INasaApiService
    {
        Task<string?> GetHighResImageUrlAsync(string nasaId);
        Task<IReadOnlyList<SearchItem>> GetSearchResult(string query, CancellationToken ct = default);
    }
}