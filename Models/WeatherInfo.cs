
using System.Text.Json.Serialization;
using System.Linq; 

namespace WeatherApp.Models
{
    
    public class WeatherInfo
    {
        [JsonPropertyName("name")]
        public string LocationName { get; set; } = "N/A"; 

        [JsonPropertyName("main")]
        public MainInfo? Main { get; set; } 

        [JsonPropertyName("weather")]
        public List<WeatherDescription>? Weather { get; set; } 

        [JsonPropertyName("wind")]
        public WindInfo? Wind { get; set; } 

        
        public string Conditions => Weather?.FirstOrDefault()?.Description ?? "N/A";
        public string IconCode => Weather?.FirstOrDefault()?.Icon ?? "01d"; 
        public string IconUrl => $"https://openweathermap.org/img/wn/{IconCode}@2x.png";
        public string TemperatureDisplay => $"{Main?.Temp ?? 0:F1}°C"; 
        public string HumidityDisplay => $"{Main?.Humidity ?? 0}%";
        public string WindSpeedDisplay => $"{Wind?.Speed ?? 0:F1} m/s"; 
    }

    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherDescription
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty; 

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty; 
    }

    public class WindInfo
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; } 
    }
}