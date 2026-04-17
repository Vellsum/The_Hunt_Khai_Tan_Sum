using The_Hunt_Khai_Tan_Sum.Services;
using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(ISettingsService settingsService)
    {
        InitializeComponent();

        _viewModel = new HomeViewModel(settingsService);
        BindingContext = _viewModel;
    }
    // for handling the case when the user interrupts the hunt by closing the app or navigating away    
    protected override void OnAppearing()
    {
        if (Preferences.Default.Get("hunt_interrupted", false))
        {
            Preferences.Default.Set("hunt_interrupted", false);

            if (BindingContext is HomeViewModel vm)
            {
                vm.StopHuntCommand.Execute(null);
            }
        }
        base.OnAppearing();

        _viewModel.RefreshAppCount();
        _viewModel.RefreshManifesto();
        _viewModel.RefreshSelectedApps();
    }
}