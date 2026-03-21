namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class GraveyardPage : ContentPage
{
    public GraveyardPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        int count = 0;
        if (TikTokSwitch.IsToggled) count++;
        if (InstagramSwitch.IsToggled) count++;
        if (MessengerSwitch.IsToggled) count++;
        if (YouTubeSwitch.IsToggled) count++;
        if (WhatsAppSwitch.IsToggled) count++;

        // Save the count for the Dashboard circle [cite: 2]
        Microsoft.Maui.Storage.Preferences.Default.Set("graveyard_count", count);

        await DisplayAlert("The HUNT", "Apps buried!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}