using The_Hunt_Khai_Tan_Sum.Views;
using The_Hunt_Khai_Tan_Sum.ViewModels;// Ensure this matches the folder you created

namespace The_Hunt_Khai_Tan_Sum.Views;
public partial class HomePage : ContentPage
{
    // Add this field
    private readonly HomeViewModel _viewModel;

    public HomePage()
    {
        InitializeComponent();

        // This connects your XAML to the MainViewModel class
        _viewModel = new HomeViewModel();
        BindingContext = _viewModel;
        
    }
    protected override void OnAppearing()
    {
       base.OnAppearing();
       _viewModel.RefreshAppCount();
    }

    private async void OnGraveyardClicked(object? sender, EventArgs e)
    {
        // Keeping your current alerts for the Assignment 1.2 submission
        await Navigation.PushAsync(new Views.GraveyardPage());
    }

    private async void OnManifestoClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.ManifestoPage());
    }

    private async void OnHistoryClicked(object? sender, EventArgs e)
    {
        await DisplayAlert("History", "Your hunt history will be displayed here.", "OK");
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        await DisplayAlert("Settings", "App setting page.", "OK");
    }

    // Added for the 'Start HUNT' button visible in your XAML
    private async void OnStartHuntClicked(object? sender, EventArgs e)

    {
        await DisplayAlert("The HUNT", "The HUNT has started!", "OK");
    }
}