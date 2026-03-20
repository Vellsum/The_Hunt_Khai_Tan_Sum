namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class GraveyardPage : ContentPage
{
    public GraveyardPage()
    {
        InitializeComponent();
    }

    // Rename this to match the 'Clicked="OnBuryClicked"' in your XAML
    private async void OnBuryClicked(object sender, EventArgs e)
    {
        int count = 0;

        // Check each CheckBox using the x:Name defined in your XAML
        if (TikTokCheck?.IsChecked == true) count++;
        if (InstagramCheck?.IsChecked == true) count++;
        if (YouTubeCheck?.IsChecked == true) count++;
        if (FaceBookCheck?.IsChecked == true) count++;

        // NATIVE STORAGE: Use the same key "graveyard_count" that your HomePage reads
        Microsoft.Maui.Storage.Preferences.Default.Set("graveyard_count", count);

        // Corrected method name: DisplayAlert (not DisplayAlertAsync)
        await DisplayAlert("Rest in Peace", $"You have buried {count} distractions.", "OK");

        // Navigate back to the Dashboard
        await Navigation.PopAsync();
    }
}