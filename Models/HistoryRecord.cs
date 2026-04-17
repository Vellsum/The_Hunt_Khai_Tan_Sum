using CommunityToolkit.Mvvm.ComponentModel;

namespace The_Hunt_Khai_Tan_Sum.Models;

// MUST inherit from ObservableObject to show the bars!
public partial class HistoryRecord : ObservableObject
{
    public string Month { get; set; } = string.Empty;
    public double RawMinutes { get; set; }
    public double ProductivityScore { get; set; }

    [ObservableProperty]
    private double _barHeight;

    [ObservableProperty]
    private bool _isMaster;
}