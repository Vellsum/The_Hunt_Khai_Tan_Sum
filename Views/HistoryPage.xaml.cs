using The_Hunt_Khai_Tan_Sum.ViewModels;

namespace The_Hunt_Khai_Tan_Sum.Views;

public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _viewModel;

    public HistoryPage()
    {
        InitializeComponent();
        _viewModel = new HistoryViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadHistory();
    }
}