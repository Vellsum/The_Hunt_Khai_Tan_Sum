using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class ManifestoViewModel : ObservableObject
{
    [ObservableProperty]
    private string _manifestoText;

    public ManifestoViewModel()
    {
        // Load saved text
        ManifestoText = Preferences.Default.Get("user_manifesto_text", "");
    }

    [RelayCommand]
    private async Task SaveManifesto()
    {
        Preferences.Default.Set("user_manifesto_text", ManifestoText);
        // We use Shell.Current for cleaner navigation in ViewModels
        await Shell.Current.DisplayAlertAsync("Saved", "Manifesto locked in!", "OK");
        await Shell.Current.Navigation.PopAsync();
    }

}