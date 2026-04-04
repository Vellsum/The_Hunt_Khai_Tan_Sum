using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        // The BindingContext connects the UI (XAML) to the Logic (C#)
        BindingContext = new SettingsViewModel();
    }
}