using The_Hunt_Khai_Tan_Sum.Models;

namespace The_Hunt_Khai_Tan_Sum.Services;

public interface ISettingsService
{
    void SaveSettings(UserSettings settings);
    UserSettings LoadSettings();
}