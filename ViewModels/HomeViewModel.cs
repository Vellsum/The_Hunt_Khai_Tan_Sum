using CommunityToolkit.Mvvm.ComponentModel; 
using CommunityToolkit.Mvvm.Input;  

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string _huntStatusText = "The HUNT is OFF";

    [ObservableProperty]
    private int _appCount;
    // Matches wireframe dashboard [cite: 1, 2]

    public void RefreshAppCount()
    {
       AppCount = Microsoft.Maui.Storage.Preferences.Default.Get("graveyard_count", 0);
       // Retrieves the count saved from GraveyardPage
    }

    [RelayCommand]
    private void ToggleHunt()
    {
        if (HuntStatusText == "The HUNT is OFF")
        {
            HuntStatusText = "The HUNT is ON";
        }
        else
        {
            HuntStatusText = "The HUNT is OFF";
        }
    }
}