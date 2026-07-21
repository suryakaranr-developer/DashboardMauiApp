namespace DashboardMauiApp.Models;

/// <summary>A single KPI/summary card shown on the Dashboard page.</summary>
public class DashboardCard
{
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Icon { get; set; } = "📊";
    public string AccentColorHex { get; set; } = "#512BD4";
    public string Trend { get; set; } = string.Empty; // e.g. "+12% vs last week"
}
