using System.Collections.ObjectModel;
using System.Windows.Input;
using DashboardMauiApp.Configuration;
using DashboardMauiApp.Models;

namespace DashboardMauiApp.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public string EnvironmentName => AppConfig.EnvironmentName;
    public string ApiBaseUrl => AppConfig.ApiBaseUrl;
    public Color BannerColor => Color.FromArgb(AppConfig.BannerColorHex);

    public ObservableCollection<DashboardCard> Cards { get; } = new();

    public ICommand RefreshCommand { get; }

    public DashboardViewModel()
    {
        Title = "Dashboard";
        RefreshCommand = new Command(async () => await LoadDataAsync());
        LoadDataAsync().ConfigureAwait(false);
    }

    async Task LoadDataAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Cards.Clear();

            // In a real app this would call ApiBaseUrl via an injected
            // HttpClient/service. Replaced here with sample data so the
            // dashboard renders immediately after a fresh clone.
            await Task.Delay(300);

            Cards.Add(new DashboardCard { Title = "Total Sales", Value = "$48,290", Icon = "💰", AccentColorHex = "#2E7D32", Trend = "+8.2% vs last week" });
            Cards.Add(new DashboardCard { Title = "Active Users", Value = "1,204", Icon = "👥", AccentColorHex = "#1565C0", Trend = "+3.1% vs last week" });
            Cards.Add(new DashboardCard { Title = "Orders", Value = "356", Icon = "📦", AccentColorHex = "#F9A825", Trend = "-1.4% vs last week" });
            Cards.Add(new DashboardCard { Title = "Revenue Growth", Value = "12.6%", Icon = "📈", AccentColorHex = "#6A1B9A", Trend = "+2.0 pts vs last month" });
        }
        finally
        {
            IsBusy = false;
        }
    }
}
