namespace WeatherApp;
using System.Diagnostics;


public partial class AddUserPage : ContentPage
{
    private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();

    public AddUserPage()
    {
        InitializeComponent();
    }

    private async void OnSaveUserClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        string email = EmailEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Name and Email cannot be empty!", "OK");
            return;
        }

        var user = new User { Name = name, Email = email };
        await _databaseHelper.AddUserAsync(user);
        await DisplayAlert("Success", "User added successfully!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
