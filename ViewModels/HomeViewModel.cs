using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    
    [ObservableProperty]
    private int _appCount ;
    // The toolkit will automatically generate public 'AppCount'
    public void RefreshAppCount()
    {
        AppCount = Microsoft.Maui.Storage.Preferences.Get("SavedAppCount", 0);
    }

}