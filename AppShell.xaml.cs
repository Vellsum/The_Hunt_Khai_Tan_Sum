using The_Hunt_Khai_Tan_Sum.Views;

namespace The_Hunt_Khai_Tan_Sum;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(GraveyardPage), typeof(GraveyardPage));
        Routing.RegisterRoute(nameof(ManifestoPage), typeof(ManifestoPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
       
    }

}