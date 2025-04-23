using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    
    public MainPage(WeatherViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Set the BindingContext
    }

    // Optional: Load weather for the default city when the page appears
    // protected override async void OnAppearing()
    // {
    //     base.OnAppearing();
    //     if (BindingContext is WeatherViewModel vm && vm.WeatherData == null && !vm.IsLoading)
    //     {
    //         // Using ?.Invoke() safely calls the command if it's not null
    //         await (vm.LoadWeatherCommand?.ExecuteAsync(null) ?? Task.CompletedTask);
    //     }
    // }
}