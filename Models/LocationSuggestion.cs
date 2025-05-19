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

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        public string DisplayName => $"{Name}, {(string.IsNullOrEmpty(State) ? Country : $"{State}, {Country}")}";
    }
}
