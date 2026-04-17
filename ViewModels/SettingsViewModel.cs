using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;
using The_Hunt_Khai_Tan_Sum.Models;
using The_Hunt_Khai_Tan_Sum.Services;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly SettingsService _settingsService = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsTimerSetupVisible))]
    private bool isTimerDelayEnabled;

    partial void OnIsTimerDelayEnabledChanged(bool value)
    {
        if (!value)
        {
            SelectedDelayTime = TimeSpan.Zero;
            SelectedHour = "00";
            SelectedMinute = "00";
            SelectedSecond = "00";
        }
    }

    [ObservableProperty]
    private bool isPoltergeistMode;

    [ObservableProperty]
    private bool isDigitalLockdownEnabled;

    [ObservableProperty]
    private bool isDarkModeEnabled;
    partial void OnIsDigitalLockdownEnabledChanged(bool value)
    {
        OnPropertyChanged(nameof(LockdownStatus));
    }

    [ObservableProperty]
    private bool isRunInBackgroundEnabled;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Hours))]
    [NotifyPropertyChangedFor(nameof(Minutes))]
    [NotifyPropertyChangedFor(nameof(Seconds))]
    private TimeSpan selectedDelayTime;

    // Timer display blocks
    public string Hours => SelectedDelayTime.Hours.ToString("D2");
    public string Minutes => SelectedDelayTime.Minutes.ToString("D2");
    public string Seconds => SelectedDelayTime.Seconds.ToString("D2");

    // UI state
    public bool IsTimerSetupVisible => IsTimerDelayEnabled;
    public string LockdownStatus => IsDigitalLockdownEnabled ? "ON" : "OFF";

    // Picker options
    public ObservableCollection<string> HourOptions { get; } =
        new(Enumerable.Range(0, 24).Select(x => x.ToString("D2")));

    public ObservableCollection<string> MinuteOptions { get; } =
        new(Enumerable.Range(0, 60).Select(x => x.ToString("D2")));

    public ObservableCollection<string> SecondOptions { get; } =
        new(Enumerable.Range(0, 60).Select(x => x.ToString("D2")));

    [ObservableProperty]
    private string selectedHour = "00";

    [ObservableProperty]
    private string selectedMinute = "00";

    [ObservableProperty]
    private string selectedSecond = "00";

    partial void OnSelectedHourChanged(string value)
    {
        UpdateSelectedDelayTime();
    }

    partial void OnSelectedMinuteChanged(string value)
    {
        UpdateSelectedDelayTime();
    }

    partial void OnSelectedSecondChanged(string value)
    {
        UpdateSelectedDelayTime();
    }

    private void UpdateSelectedDelayTime()
    {
        if (int.TryParse(SelectedHour, out int h) &&
            int.TryParse(SelectedMinute, out int m) &&
            int.TryParse(SelectedSecond, out int s))
        {
            SelectedDelayTime = new TimeSpan(h, m, s);
        }
    }

    public SettingsViewModel()
    {
        LoadSettings();
        CheckNotificationPermission();
    }
    //dark mode toggle handler
    partial void OnIsDarkModeEnabledChanged(bool value)
    {
        Preferences.Set("dark_mode", value);

        Application.Current.UserAppTheme = value
            ? AppTheme.Dark
            : AppTheme.Light;
    }

    // for loading Settings
    public void LoadSettings()
    {
        var settings = _settingsService.LoadSettings();

        IsTimerDelayEnabled = settings.IsTimerDelayEnabled;
        SelectedDelayTime = settings.SelectedDelayTime;
        IsPoltergeistMode = settings.IsPoltergeistMode;
        IsDigitalLockdownEnabled = settings.IsDigitalLockdownEnabled;
        IsRunInBackgroundEnabled = settings.IsRunInBackgroundEnabled;

        IsDarkModeEnabled = Preferences.Get("dark_mode", false);

        SelectedHour = SelectedDelayTime.Hours.ToString("D2");
        SelectedMinute = SelectedDelayTime.Minutes.ToString("D2");
        SelectedSecond = SelectedDelayTime.Seconds.ToString("D2");
    }

    private async void CheckNotificationPermission()
    {
        try
        {
            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
                await LocalNotificationCenter.Current.RequestNotificationPermission();
        }
        catch (Exception)
        {
            // Ignore permission check errors for now
        }
    }

    // saving settings
    [RelayCommand]
    private async Task SaveSettings()
    {
        Preferences.Set("mode", IsPoltergeistMode ? "POLTERGEIST" : "WHISPER");

        if (IsTimerDelayEnabled && SelectedDelayTime.TotalSeconds <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Invalid Timer", "Please set a delay time.", "OK");
            return;
        }


        var settings = new UserSettings
        {
            IsTimerDelayEnabled = IsTimerDelayEnabled,
            SelectedDelayTime = SelectedDelayTime,
            IsPoltergeistMode = IsPoltergeistMode,
            IsDigitalLockdownEnabled = IsDigitalLockdownEnabled,
            IsRunInBackgroundEnabled = IsRunInBackgroundEnabled
        };

        _settingsService.SaveSettings(settings);

        Preferences.Set("timerValue", SelectedDelayTime.TotalSeconds);

        await Shell.Current.DisplayAlertAsync("Saved", "Settings saved successfully.", "OK");
        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
    // for the UI look more clear(neat) fix and remove this clearHistory()

    //[RelayCommand]
    //private async Task ClearHistory()
    //{
       // bool confirmed = await Shell.Current.DisplayAlert(
        //    "Clear History & Data",
        //    "This will permanently delete all your usage history. Are you sure?",
         //   "Yes, Clear",
         //   "Cancel");

       // if (!confirmed)
         //   return;

      //  var now = DateTime.Now;
      //  for (int i = 0; i < 24; i++)
       // {
        //    var date = now.AddMonths(-i);
         //   Preferences.Remove($"usage_{date.Year}_{date.Month:D2}");
      //  }
       // Preferences.Remove("total_hunt_minutes");
      //  Preferences.Remove("graveyard_count");

       // await Shell.Current.DisplayAlert("Done", "History and data cleared.", "OK");
   // }


