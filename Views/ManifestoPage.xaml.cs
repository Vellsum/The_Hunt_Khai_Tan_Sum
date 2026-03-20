using The_Hunt_Khai_Tan_Sum.ViewModels; // Ensure this matches the folder you created

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class ManifestoPage : ContentPage
{
    public ManifestoPage()
    {
        InitializeComponent();
        //linking viewmodel
        BindingContext = new ManifestoViewModels();
    }
}