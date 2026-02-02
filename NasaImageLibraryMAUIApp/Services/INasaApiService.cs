using NasaImageLibraryMAUIApp.Models;

namespace NasaImageLibraryMAUIApp.Services
{
    public interface INasaApiService
    {
        Task<string?> GetHighResImageUrlAsync(string nasaId);
        Task<IReadOnlyList<SearchItem>> GetSearchResult(string query, int? startYear = null, int? endYear = null, CancellationToken ct = default);
    }
}