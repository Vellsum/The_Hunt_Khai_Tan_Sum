using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;  
using Microsoft.Maui.Controls; // if this is a .NET MAUI project
// using Xamarin.Forms; // if this is a Xamarin.Forms project

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class ManifestoViewModels : ObservableObject
{
    [ObservableProperty]
    private string _userMessage = string.Empty;

    [RelayCommand]
    private async Task SaveManifesto()
    {
       if(string.IsNullOrWhiteSpace(UserMessage))
       {
           await Application.Current.MainPage.DisplayAlert("Error", "Please enter a message before saving.", "OK");
           return;
       }
        await Application.Current.MainPage.DisplayAlert("Saved", "Your Manifesto is set for the next Hunt!", "OK");
    }
}
