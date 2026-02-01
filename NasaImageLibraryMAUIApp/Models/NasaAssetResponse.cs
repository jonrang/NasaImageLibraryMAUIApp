using System.Text.Json.Serialization;

namespace NasaImageLibraryMAUIApp.Models
{
    public class NasaAssetResponse
    {
        [JsonPropertyName("collection")]
        public AssetCollection Collection { get; set; }
    }

    public class AssetCollection
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<AssetItem> Items { get; set; }
    }

    public class AssetItem
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

}