
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using System.Collections.ObjectModel; 
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq; 

namespace WeatherApp.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        private readonly WeatherService _weatherService;
        private CancellationTokenSource _suggestionCts = new(); 

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        private bool _isLoading;

      
        private string _city = ""; 
        public string City
        {
            get => _city;
            set
            {
                if (SetProperty(ref _city, value)) 
                {
                   
                    OnCityChanged(value);
                    // Optionally clear weather data when city changes manually
                    // WeatherData = null;
                    // ErrorMessage = null;
                }
            }
        }
       

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasData))]
        private WeatherInfo? _weatherData;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string? _errorMessage;

       
        [ObservableProperty]
        private ObservableCollection<LocationSuggestion> _suggestions = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSuggestions))]
        private bool _isSuggestionListVisible = false; 

        public bool HasSuggestions => Suggestions.Any();
       

        public bool IsNotLoading => !IsLoading;

        public string City_Location => City; 
        public bool HasData => WeatherData != null && string.IsNullOrEmpty(ErrorMessage);
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public WeatherViewModel(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

      
        private async void OnCityChanged(string newCityValue)
        {
          
            _suggestionCts.Cancel();
            _suggestionCts = new CancellationTokenSource(); 
            var cancellationToken = _suggestionCts.Token;

            Debug.WriteLine("Here " + Suggestions);
         
            if (Suggestions.Any())
            {
               Suggestions.Clear();
               IsSuggestionListVisible = false; 
               OnPropertyChanged(nameof(HasSuggestions));
            }

            if (string.IsNullOrWhiteSpace(newCityValue) || newCityValue.Length < 3)
            {
               
                 IsSuggestionListVisible = false; 
                return;
            }


            try
            {
              
                await Task.Delay(500, cancellationToken);

              
                Debug.WriteLine($"Fetching suggestions for: {newCityValue}");
                var fetchedSuggestions = await _weatherService.GetLocationSuggestionsAsync(newCityValue);

                 if (cancellationToken.IsCancellationRequested) return; 

                Suggestions.Clear(); 
                if (fetchedSuggestions != null && fetchedSuggestions.Any())
                {
                    foreach (var suggestion in fetchedSuggestions)
                    {
                        Suggestions.Add(suggestion);
                    }
                    IsSuggestionListVisible = true; 
                }
                else
                {
                     IsSuggestionListVisible = false; 
                }
                 OnPropertyChanged(nameof(HasSuggestions)); 
            }
            catch (TaskCanceledException)
            {
              
                Debug.WriteLine("Suggestion fetch cancelled.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching suggestions: {ex.Message}");
                IsSuggestionListVisible = false; 
              
            }
        }
       
        [RelayCommand]
        private async Task LoadWeatherAsync(string? cityToLoad = null)
        {
            if (IsLoading) return;

            IsLoading = true;
            ErrorMessage = null;
            WeatherData = null;
            IsSuggestionListVisible = false;

          
            string targetCity = cityToLoad ?? City;

            if (string.IsNullOrWhiteSpace(targetCity))
            {
                ErrorMessage = "Please enter a city name.";
                IsLoading = false;
                OnPropertyChanged(nameof(ErrorMessage)); 
                OnPropertyChanged(nameof(HasError));
                return;
            }

             
            Debug.WriteLine($"Loading weather for: {targetCity}"); 

            try
            {
                WeatherData = await _weatherService.GetWeatherAsync(targetCity);

                Debug.WriteLine($"Weather data loaded: {City_Location}");

                if (WeatherData == null && string.IsNullOrEmpty(ErrorMessage))
                {
                     ErrorMessage = $"Could not retrieve weather for '{targetCity}'. Check location format/connection.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                Debug.WriteLine($"Error in LoadWeatherAsync: {ex}");
            }
            finally
            {
                IsLoading = false;
                
                 OnPropertyChanged(nameof(HasError));
                 OnPropertyChanged(nameof(HasData));
            }
        }

       
        [RelayCommand]
        private void SelectSuggestion(LocationSuggestion? selectedSuggestion)
        {
            if (selectedSuggestion != null)
            {
                
                _city = selectedSuggestion.DisplayName; 
             

                OnPropertyChanged(nameof(City)); 

                Suggestions.Clear();
                IsSuggestionListVisible = false;
                OnPropertyChanged(nameof(HasSuggestions));

                LoadWeatherCommand.Execute(selectedSuggestion.DisplayName);
                
            }
        }
        
    }
}