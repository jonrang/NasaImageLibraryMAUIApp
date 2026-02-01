using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using NasaImageLibraryMAUIApp.Models;

namespace NasaImageLibraryMAUIApp.Services
{
    public class NasaApiService : INasaApiService
    {
        private readonly HttpClient http;

        public NasaApiService(HttpClient http)
        {
            this.http = http;
        }


        public async Task<IReadOnlyList<SearchItem>> GetSearchResult(string query, CancellationToken ct = default)
        {
            var encodedQuery = Uri.EscapeDataString(query);

            var url = $"https://images-api.nasa.gov/search?q={encodedQuery}&media_type=image";

            using var response = await http.GetAsync(url, ct);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"NasaAPI-fel {(int)response.StatusCode}: {body}");
            }
            var dto = await response.Content.ReadFromJsonAsync<SearchResponse>(cancellationToken: ct);
            return dto?.Collection?.Items ?? new List<SearchItem>();
        }
    }
}
