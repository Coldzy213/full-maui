namespace WeatherApp;

public partial class UpdateUserPage : ContentPage
{
    private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();
    private User _user;

    public UpdateUserPage(User user)
    {
        InitializeComponent();
        _user = user;
        NameEntry.Text = _user.Name;
        EmailEntry.Text = _user.Email; // Load existing email
    }

    private async void OnUpdateUserClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameEntry.Text) && !string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            _user.Name = NameEntry.Text;
            _user.Email = EmailEntry.Text; // Update email
            await _databaseHelper.UpdateUserAsync(_user);
            await DisplayAlert("Success", "User updated successfully!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Name and Email cannot be empty!", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
