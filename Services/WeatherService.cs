
using WeatherApp.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic; 
using System.Linq; 

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
       
        private const string ApiKey = "12dd4f965683b2513dd83aa80b132d2a";
      
        private const string WeatherBaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string GeocodeBaseUrl = "https://api.openweathermap.org/geo/1.0/direct"; 

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
          
            // _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        
        public async Task<WeatherInfo?> GetWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city) || ApiKey == "YOUR_OPENWEATHERMAP_API_KEY")
            {
                Debug.WriteLine("Weather Error: City is empty or API Key is not set.");
                return null;
            }
            string requestUri = $"{WeatherBaseUrl}?q={Uri.EscapeDataString(city)}&appid={ApiKey}&units=metric";
          
             try
            {
                WeatherInfo? weatherInfo = await _httpClient.GetFromJsonAsync<WeatherInfo>(requestUri);
                return weatherInfo;
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Debug.WriteLine($"Weather Error: City not found '{city}'. {httpEx.Message}");
                return null;
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"Weather Network Error: {httpEx.Message} (Status Code: {httpEx.StatusCode})");
                return null;
            }
            catch (NotSupportedException jsonEx)
            {
                Debug.WriteLine($"Weather JSON Error: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Weather Unexpected Error: {ex.Message}");
                return null;
            }
        }


       
        public async Task<List<LocationSuggestion>> GetLocationSuggestionsAsync(string query, int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 3 || ApiKey == "YOUR_OPENWEATHERMAP_API_KEY")
            {
               
                return new List<LocationSuggestion>(); 
            }

            string requestUri = $"{GeocodeBaseUrl}?q={Uri.EscapeDataString(query)}&limit={limit}&appid={ApiKey}";

            try
            {
               
                List<LocationSuggestion>? suggestions = await _httpClient.GetFromJsonAsync<List<LocationSuggestion>>(requestUri);
                return suggestions ?? new List<LocationSuggestion>(); 
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"Geocoding Network Error: {httpEx.Message} (Status Code: {httpEx.StatusCode})");
                return new List<LocationSuggestion>(); 
            }
            catch (NotSupportedException jsonEx)
            {
                Debug.WriteLine($"Geocoding JSON Error: {jsonEx.Message}");
                 return new List<LocationSuggestion>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Geocoding Unexpected Error: {ex.Message}");
                return new List<LocationSuggestion>(); 
            }
        }
       
    }
}