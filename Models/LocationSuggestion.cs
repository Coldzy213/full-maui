
using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class LocationSuggestion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string? State { get; set; } 

        
        public string DisplayName => $"{Name}, {(string.IsNullOrEmpty(State) ? Country : $"{State}, {Country}")}";
    }
}