using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class GraveyardPage : ContentPage
{
    public GraveyardPage(GraveyardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}