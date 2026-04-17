using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using The_Hunt_Khai_Tan_Sum.Models;

namespace The_Hunt_Khai_Tan_Sum.ViewModels;

public partial class GraveyardViewModel : ObservableObject
{
    public ObservableCollection<AppItem> Apps { get; set; } = new();

    public GraveyardViewModel()
    {
        LoadApps();
    }

    private void LoadApps()
    {
        var saved = Preferences.Default.Get("graveyard_apps", "");

        if (!string.IsNullOrWhiteSpace(saved))
        {
            var loadedApps = JsonSerializer.Deserialize<List<AppItem>>(saved);
            if (loadedApps != null)
            {
                // safety: older saved data may not have DarkIcon yet
                foreach (var app in loadedApps)
                {
                    if (string.IsNullOrWhiteSpace(app.DarkIcon))
                    {
                        app.DarkIcon = app.Icon;
                    }
                }

                Apps = new ObservableCollection<AppItem>(loadedApps);
                OnPropertyChanged(nameof(Apps));
                return;
            }
        }

        Apps = new ObservableCollection<AppItem>
        {
            new AppItem
            {
                Name = "TikTok",
                Icon = "tiktok.png",
                DarkIcon = "tiktok.png",
                IsSelected = false
            },
            new AppItem
            {
                Name = "Instragram",
                Icon = "instragram.png",
                DarkIcon = "instragram.png",
                IsSelected = false
            },
            new AppItem
            {
                Name = "Facebook",
                Icon = "facebook.png",
                DarkIcon = "facebook.png",
                IsSelected = false
            },
            new AppItem
            {
                Name = "YouTube",
                Icon = "youtube.png",
                DarkIcon = "youtube_white.jpeg",
                IsSelected = false
            },
            new AppItem
            {
                Name = "WhatsApp",
                Icon = "whatsapp.png",
                DarkIcon = "whatsapp.png",
                IsSelected = false
            }
        };

        OnPropertyChanged(nameof(Apps));
    }

    [RelayCommand]
    private async Task SaveApps()
    {
        var json = JsonSerializer.Serialize(Apps.ToList());
        Preferences.Default.Set("graveyard_apps", json);

        int selectedCount = Apps.Count(x => x.IsSelected);
        Preferences.Default.Set("graveyard_count", selectedCount);

        await Shell.Current.DisplayAlertAsync("Saved", "Graveyard selections saved.", "OK");
        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}