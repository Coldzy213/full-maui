namespace WeatherApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

private void OnToggleThemeClicked(object sender, EventArgs e)
{
	if (Application.Current.UserAppTheme == AppTheme.Light)
		Application.Current.UserAppTheme = AppTheme.Dark;
	else
		Application.Current.UserAppTheme = AppTheme.Light;
}

}
