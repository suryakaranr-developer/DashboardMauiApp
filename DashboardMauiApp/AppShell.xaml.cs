namespace DashboardMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.DashboardPage), typeof(Views.DashboardPage));
    }
}
