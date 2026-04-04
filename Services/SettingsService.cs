using The_Hunt_Khai_Tan_Sum.Models;
using Microsoft.Maui.Storage;

namespace The_Hunt_Khai_Tan_Sum.Services;

public class SettingsService
{
    public void SaveSettings(UserSettings settings)
    {
        Preferences.Default.Set("timer_delay", settings.IsTimerDelayEnabled);
        Preferences.Default.Set("delay_seconds", settings.SelectedDelayTime.TotalSeconds);
        Preferences.Default.Set("poltergeist", settings.IsPoltergeistMode);
        Preferences.Default.Set("lockdown", settings.IsDigitalLockdownEnabled);
        Preferences.Default.Set("background", settings.IsRunInBackgroundEnabled);
    }

    public UserSettings LoadSettings()
    {
        return new UserSettings
        {
            IsTimerDelayEnabled = Preferences.Default.Get("timer_delay", false),
            SelectedDelayTime = TimeSpan.FromSeconds(Preferences.Default.Get("delay_seconds", 60.0)),
            IsPoltergeistMode = Preferences.Default.Get("poltergeist", false),
            IsDigitalLockdownEnabled = Preferences.Default.Get("lockdown", false),
            IsRunInBackgroundEnabled = Preferences.Default.Get("background", true)
        };
    }
}