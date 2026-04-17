using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = new SettingsViewModel();
    }
}