using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using The_Hunt_Khai_Tan_Sum.Services;
using The_Hunt_Khai_Tan_Sum.Views;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private readonly NotificationService _notificationService = new();
    private System.Timers.Timer? _countdownTimer;

    public HomeViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        RefreshAppCount();
        RefreshManifesto();
        RefreshSelectedApps();
    }

    [ObservableProperty]
    private string huntStatusText = "Ready to Focus?";

    [ObservableProperty]
    private int appCount;

    // show the selected apps in the overlay
    private string _selectedAppsText = "";
    public string SelectedAppsText
    {
        get => _selectedAppsText;
        set => SetProperty(ref _selectedAppsText, value);
    }

    // True while countdown is running
    [ObservableProperty]
    private bool isHuntActive;

    private bool showOverlay;
    public bool ShowOverlay
    {
        get => showOverlay;
        set => SetProperty(ref showOverlay, value);
    }

    [ObservableProperty]
    private string displayManifesto = "";

    [ObservableProperty]
    private string remainingTime = "00:00:00";
    //to validate if user has selected apps or manifesto 
    private bool HasSelectedApps()
    {
        return Preferences.Default.Get("graveyard_count", 0) > 0;
    }

    private bool HasManifesto()
    {
        var manifesto = Preferences.Default.Get("manifesto", "");
        return !string.IsNullOrWhiteSpace(manifesto);
    }
    public void RefreshAppCount()
    {
        AppCount = Preferences.Default.Get("graveyard_count", 0);
    }

    public void RefreshManifesto()
    {
        DisplayManifesto = Preferences.Default.Get(
            "manifesto",
            "You chose this. Don’t break it now.");
    }

    [RelayCommand]
    private async Task OpenGraveyard()
    {
        if (IsHuntActive)
        {
            await Shell.Current.DisplayAlertAsync("HUNT Active", "Finish the current HUNT first.", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(GraveyardPage));
    }

    [RelayCommand]
    private async Task OpenManifesto()
    {
        if (IsHuntActive)
        {
            await Shell.Current.DisplayAlertAsync("HUNT Active", "Finish the current HUNT first.", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(ManifestoPage));
    }

    [RelayCommand]
    private async Task OpenHistory()
    {
        if (IsHuntActive)
        {
            await Shell.Current.DisplayAlertAsync("HUNT Active", "Finish the current HUNT first.", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(HistoryPage));
    }

    [RelayCommand]
    private async Task OpenSettings()
    {
        if (IsHuntActive)
        {
            await Shell.Current.DisplayAlertAsync("HUNT Active", "Finish the current HUNT first.", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    [RelayCommand]
    private async Task StartHunt()
    {
        if (IsHuntActive)
        {
            await Shell.Current.DisplayAlertAsync("Already Running", "The HUNT is already active.", "OK");
            return;
        }

        if (!HasSelectedApps())
        {
            await Shell.Current.DisplayAlertAsync("No Apps Selected", "Please choose at least one app in Graveyard.", "OK");
            return;
        }

        if (!HasManifesto())
        {
            await Shell.Current.DisplayAlertAsync("No Manifesto", "Please write your future-self message first.", "OK");
            return;
        }

        var settings = _settingsService.LoadSettings();

        if (!settings.IsTimerDelayEnabled)
        {
            await Shell.Current.DisplayAlertAsync(
                "Timer Required",
                "Please turn on Timer Delay in Settings before starting THE HUNT.",
                "OK");
            return;
        }

        TimeSpan delay = settings.SelectedDelayTime;

        if (delay.TotalSeconds <= 0)
        {
            await Shell.Current.DisplayAlertAsync(
                "Invalid Timer",
                "Please set a valid timer in Settings.",
                "OK");
            return;
        }

        //  Refresh data BEFORE starting
        RefreshManifesto();
        RefreshSelectedApps();

        //  Save session info
        Preferences.Default.Set("last_manifesto", DisplayManifesto);
        Preferences.Default.Set("last_selected_apps_text", SelectedAppsText);

        //Start UI state
        IsHuntActive = true;
        ShowOverlay = false;
        HuntStatusText = "⚠ HUNT ACTIVE";
        RemainingTime = delay.ToString(@"hh\:mm\:ss");

        //Start countdown LAST
        StartCountdown(delay);
    }
    // for the overlay to show the selected apps
    public void RefreshSelectedApps()
    {
        var saved = Preferences.Default.Get("graveyard_apps", "");

        if (string.IsNullOrWhiteSpace(saved))
        {
            SelectedAppsText = "No apps selected.";
            return;
        }

        try
        {
            var apps = System.Text.Json.JsonSerializer.Deserialize<List<The_Hunt_Khai_Tan_Sum.Models.AppItem>>(saved);

            if (apps == null)
            {
                SelectedAppsText = "No apps selected.";
                return;
            }

            var selected = apps
                .Where(a => a.IsSelected)
                .Select(a => a.Name)
                .ToList();

            SelectedAppsText = selected.Count > 0
                ? $"Apps to avoid: {string.Join(", ", selected)}"
                : "No apps selected.";
        }
        catch
        {
            SelectedAppsText = "No apps selected.";
        }
    }
    // for the countdown timer and to update the overlay time remaining
    private void StartCountdown(TimeSpan duration)
    {
        var endTime = DateTime.Now.Add(duration);

        _countdownTimer?.Stop();
        _countdownTimer?.Dispose();

        _countdownTimer = new System.Timers.Timer(1000);
        _countdownTimer.Elapsed += async (s, e) =>
        {
            var remaining = endTime - DateTime.Now;

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (remaining.TotalSeconds <= 0)
                {
                    _countdownTimer?.Stop();
                    _countdownTimer?.Dispose();
                    _countdownTimer = null;

                    RemainingTime = "00:00:00";
                    HuntStatusText = "The HUNT is OFF";

                    RefreshManifesto();

                    IsHuntActive = false;
                    ShowOverlay = true;

                   await _notificationService.ShowImmediateHuntNotification();
                    return;
                }

                RemainingTime = remaining.ToString(@"hh\:mm\:ss");
            });
        };

        _countdownTimer.Start();
    }

    [RelayCommand]
    private async Task StopHunt()
    {
        var settings = _settingsService.LoadSettings();

        if (settings.IsDigitalLockdownEnabled)
        {
            bool confirm = await Shell.Current.DisplayAlertAsync(
                "Stay Focused",
                "Are you sure you want to stop THE HUNT?",
                "Yes",
                "Continue HUNT");

            if (!confirm)
                return;
        }

        _countdownTimer?.Stop();
        _countdownTimer?.Dispose();
        _countdownTimer = null;

        IsHuntActive = false;
        ShowOverlay = false;
        HuntStatusText = "Ready to Focus?";
        RemainingTime = "00:00:00";
        DisplayManifesto = "";
    }
}