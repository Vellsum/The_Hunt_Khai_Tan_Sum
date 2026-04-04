using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage()
    {
        InitializeComponent();

        // Connects the ViewModel to the View
        _viewModel = new HomeViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Updates the "3" count from  wireframe every time when open the page [cite: 1, 2]
        _viewModel.RefreshAppCount();
    }

    private async void OnGraveyardClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new GraveyardPage());
    }

    private async void OnManifestoClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManifestoPage());
    }

    private async void OnHistoryClicked(object? sender, EventArgs e)
    {
        await DisplayAlertAsync("History", "Your hunt history will be displayed here.", "OK");
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }

    private async void OnStartHuntClicked(object? sender, EventArgs e)
    {

        // Matches the "Start HUNT" button in your wireframe [cite: 1, 9]
        await DisplayAlertAsync("The HUNT", "The HUNT has started!", "OK");
    }
}