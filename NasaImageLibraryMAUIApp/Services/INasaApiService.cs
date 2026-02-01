using NasaImageLibraryMAUIApp.Models;

namespace NasaImageLibraryMAUIApp.Services
{
    public interface INasaApiService
    {
        Task<IReadOnlyList<SearchItem>> GetSearchResult(string query, CancellationToken ct = default);
    }
}