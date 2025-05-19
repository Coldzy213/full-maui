using WeatherApp.ViewModels;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    private WeatherViewModel _viewModel;

    public MainPage(WeatherViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        // Subscribe to property changes for location updates
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.Latitude) || e.PropertyName == nameof(_viewModel.Longitude))
        {
            UpdateMap(_viewModel.Latitude, _viewModel.Longitude);
        }
    }

    private void UpdateMap(double latitude, double longitude)
    {
        var html = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <link rel='stylesheet' href='https://unpkg.com/leaflet@1.7.1/dist/leaflet.css' />
                <script src='https://unpkg.com/leaflet@1.7.1/dist/leaflet.js'></script>
                <style> html, body, #map {{ height: 100%; margin: 0; }} </style>
            </head>
            <body>
                <div id='map'></div>
                <script>
                    var map = L.map('map').setView([{latitude}, {longitude}], 10);
                    L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
                        maxZoom: 19
                    }}).addTo(map);
                    L.marker([{latitude}, {longitude}]).addTo(map)
                        .bindPopup('Selected Location')
                        .openPopup();
                </script>
            </body>
            </html>";

        osmWebView.Source = new HtmlWebViewSource { Html = html };
    }
}
