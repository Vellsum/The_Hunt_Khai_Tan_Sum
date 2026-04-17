using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class HistoryViewModel : ObservableObject
{
    public ObservableCollection<MonthlyUsage> UsageData { get; } = new();

    [ObservableProperty]
    private string totalMinutesText = "0 minutes";

    public HistoryViewModel()
    {
        LoadHistory();
    }

    public void LoadHistory()
    {
        UsageData.Clear();

        var now = DateTime.Now;

        for (int i = 2; i >= 0; i--)
        {
            var date = now.AddMonths(-i);
            string key = $"usage_{date.Year}_{date.Month:D2}";
            int count = Preferences.Get(key, 0);

            UsageData.Add(new MonthlyUsage
            {
                Month = date.ToString("MMM").ToUpper(),
                Count = count
            });
        }

        double totalMinutes = Preferences.Get("total_hunt_minutes", 0.0);
        TotalMinutesText = $"Total Reminder Session Time: {Math.Round(totalMinutes, 1)} minutes";
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task ShareProgress()
    {
        double totalMinutes = Preferences.Get("total_hunt_minutes", 0.0);

        var now = DateTime.Now;
        string key = $"usage_{now.Year}_{now.Month:D2}";
        int currentMonthCount = Preferences.Get(key, 0);

        string message = $"I completed {currentMonthCount} HUNT reminder sessions this month using THE HUNT.\n" +
                         $"Total reminder session time: {Math.Round(totalMinutes, 1)} minutes.";

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = message,
            Title = "My HUNT Progress"
        });
    }
}

public class MonthlyUsage
{
    public string Month { get; set; } = "";
    public int Count { get; set; }
}