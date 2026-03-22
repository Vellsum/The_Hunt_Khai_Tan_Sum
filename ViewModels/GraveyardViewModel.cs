using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;


public partial class GraveyardViewModel : ObservableObject
{
    [ObservableProperty]
    private int _appCount;

    public GraveyardViewModel()
    {
        // load the  saved count
        AppCount = Preferences.Default.Get("graveyard_count", 0);
    }

    [RelayCommand]
    private void AddApp()
    {
        AppCount++;
        Preferences.Default.Set("graveyard_count", AppCount);
    }

    [RelayCommand]
    private void ResetGraveyard()
    {
        AppCount = 0;
        Preferences.Default.Set("graveyard_count", 0);
    }
}