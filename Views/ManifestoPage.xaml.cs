using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class ManifestoPage : ContentPage
{
    public ManifestoPage()
    {
        InitializeComponent();
        BindingContext = new ManifestoViewModel();
    }
}