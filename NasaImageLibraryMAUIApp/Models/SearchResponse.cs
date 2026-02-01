using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NasaImageLibraryMAUIApp.Models
{
    public class SearchResponse
    {
        [JsonPropertyName("collection")]
        public SearchCollection Collection { get; set; }
    }

    public class SearchCollection
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<SearchItem> Items { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }
    }

    public class Datum
    {
        [JsonPropertyName("center")]
        public string Center { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime? DateCreated { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("description_508")]
        public string Description508 { get; set; }

        [JsonPropertyName("keywords")]
        public List<string> Keywords { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }

        [JsonPropertyName("nasa_id")]
        public string NasaId { get; set; }

        [JsonPropertyName("secondary_creator")]
        public string SecondaryCreator { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("photographer")]
        public string Photographer { get; set; }

        [JsonPropertyName("album")]
        public List<string> Album { get; set; }
    }

    public class SearchItem
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("data")]
        public List<Datum> Data { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("render")]
        public string Render { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("size")]
        public int? Size { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("total_hits")]
        public int? TotalHits { get; set; }
    }
}