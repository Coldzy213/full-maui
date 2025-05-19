namespace WeatherApp;
using System.Diagnostics;

public partial class CrudPage : ContentPage
{
    public CrudPage()
    {
        InitializeComponent();
         InitializeDatabase();
    }

    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddUserPage());
    }

    private async void OnShowUsersClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new ShowUsersPage());
    }

    private async void InitializeDatabase()
    {
        var db = new DatabaseHelper();
        await db.InitializeDatabaseAsync();
    }
}
