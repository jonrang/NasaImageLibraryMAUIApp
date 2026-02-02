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


        public async Task<IReadOnlyList<SearchItem>> GetSearchResult(
            string query, 
            int? startYear = null, 
            int? endYear = null, 
            CancellationToken ct = default)
        {
            var encodedQuery = Uri.EscapeDataString(query);

            var sb = new StringBuilder($"search?q={encodedQuery}&media_type=image");
            
            if (startYear.HasValue) sb.Append($"&year_start={startYear.Value}");

            if(endYear.HasValue) sb.Append($"&year_end={endYear.Value}");
            
            var url = sb.ToString();

            using var response = await http.GetAsync(url, ct);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"NasaAPI-fel {(int)response.StatusCode}: {body}");
            }
            var dto = await response.Content.ReadFromJsonAsync<SearchResponse>(cancellationToken: ct);
            return dto?.Collection?.Items ?? new List<SearchItem>();
        }

        public async Task<string?> GetHighResImageUrlAsync(string nasaId)
        {
            var url = $"asset/{nasaId}";

            try
            {
                var response = await http.GetFromJsonAsync<NasaAssetResponse>(url);

                var items = response?.Collection?.Items;
                if (items is null || items.Count == 0) return null;

                var bestMatch = items.FirstOrDefault(x => x.Href.EndsWith("~medium.jpg"))
                    ?? items.FirstOrDefault(x => x.Href.EndsWith("~small.jpg"))
                    ?? items.FirstOrDefault();

                string? originalUrl = bestMatch?.Href;

                if (!string.IsNullOrEmpty(originalUrl) && originalUrl.StartsWith("http:"))
                {
                    return originalUrl.Replace("http:", "https:");
                }
                
                return originalUrl;
            }
            catch
            {
                return null;
            }
        }
    }
}
