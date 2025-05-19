namespace WeatherApp;
using System.Diagnostics;

public partial class ShowUsersPage : ContentPage
{
    private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();

    public ShowUsersPage()
    {
        InitializeComponent();
        LoadUsers();
    }

    private async void LoadUsers()
    {
        var users = await _databaseHelper.GetUsersAsync();
        UsersListView.ItemsSource = users;
    }

private async void OnUpdateUserClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var user = button?.CommandParameter as User;

        if (user != null)
        {
            await Navigation.PushAsync(new UpdateUserPage(user));
        }
    }

     private async void OnDeleteUserClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var user = button?.CommandParameter as User;

        if (user != null)
        {
            bool confirm = await DisplayAlert("Delete", $"Are you sure you want to delete {user.Name}?", "Yes", "No");
            if (confirm)
            {
                await _databaseHelper.DeleteUserAsync(user);
                LoadUsers(); // Refresh list
            }
        }
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
