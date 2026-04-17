using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class ManifestoViewModel : ObservableObject
{
    [ObservableProperty]
    private string manifestoText = "";

    public ManifestoViewModel()
    {
        ManifestoText = Preferences.Default.Get("manifesto", "");
    }

    [RelayCommand]
    private async Task SaveManifesto()
    {
        if (string.IsNullOrWhiteSpace(ManifestoText))
        {
            await Shell.Current.DisplayAlertAsync(
                "Empty Message",
                "Please write your future-self message first.",
                "OK");
            return;
        }

        Preferences.Default.Set("manifesto", ManifestoText);

        await Shell.Current.DisplayAlertAsync(
            "Saved",
            "Manifesto locked in!",
            "OK");

        await Shell.Current.Navigation.PopAsync();
    }
}