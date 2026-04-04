using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using Services;
using The_Hunt_Khai_Tan_Sum.Models;
using The_Hunt_Khai_Tan_Sum.Services;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    
    private readonly SettingsService _settingsService = new();

    private readonly NotificationService _notificationService = new();
    //add the noti service
    [ObservableProperty]
    private bool _isTimerDelayEnabled;

    [ObservableProperty]
    private bool _isPoltergeistMode;

    [ObservableProperty]
    private bool _isDigitalLockdownEnabled;

    [ObservableProperty]
    private bool _isRunInBackgroundEnabled;

    [ObservableProperty]
    private TimeSpan _selectedDelayTime;

    // UI Visibility Logic
    public bool IsTimerSetupVisible => IsTimerDelayEnabled;

    public SettingsViewModel()
    {
        
        var settings = _settingsService.LoadSettings();

        IsTimerDelayEnabled = settings.IsTimerDelayEnabled;
        IsPoltergeistMode = settings.IsPoltergeistMode;
        IsDigitalLockdownEnabled = settings.IsDigitalLockdownEnabled;
        IsRunInBackgroundEnabled = settings.IsRunInBackgroundEnabled;
        SelectedDelayTime = settings.SelectedDelayTime;

        CheckNotificationPermission();
        //Request permission as soon as the user enter settings
    }

    private async void CheckNotificationPermission()
    {
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }
    }

    partial void OnIsTimerDelayEnabledChanged(bool value) => OnPropertyChanged(nameof(IsTimerSetupVisible));

    [RelayCommand]
    private async Task SaveSettings()
    {
        var settingsToSave = new UserSettings
        {
            IsTimerDelayEnabled = this.IsTimerDelayEnabled,
            SelectedDelayTime = this.SelectedDelayTime,
            IsPoltergeistMode = this.IsPoltergeistMode,
            IsDigitalLockdownEnabled = this.IsDigitalLockdownEnabled,
            IsRunInBackgroundEnabled = this.IsRunInBackgroundEnabled
        };

        _settingsService.SaveSettings(settingsToSave);
        // schedule the notification if timer is turned on
        if (IsTimerDelayEnabled)
        {
            _notificationService.ScheduleHuntNotification(SelectedDelayTime);
        }
        await Shell.Current.Navigation.PopAsync();
    }


    [RelayCommand]
    private async Task Back() => await Shell.Current.Navigation.PopAsync();
}