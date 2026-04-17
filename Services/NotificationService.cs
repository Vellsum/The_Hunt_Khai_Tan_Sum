using System.Threading.Tasks;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using Microsoft.Maui.Devices;

namespace The_Hunt_Khai_Tan_Sum.Services;

public class NotificationService
{
    private const int NotificationId = 1001;

    public void ScheduleHuntNotification(TimeSpan delay)
    {
        var message = Preferences.Default.Get("manifesto", "Stop. Is this distraction worth it?");
        var mode = Preferences.Default.Get("mode", "WHISPER");

        string title = mode == "POLTERGEIST" ? "⚠️ THE HUNT" : "👁️ Gentle Reminder";
        string finalMessage = mode == "POLTERGEIST"
            ? $"STOP.\n{message.ToUpper()}"
            : message;

        var request = new NotificationRequest
        {
            NotificationId = NotificationId,
            Title = title,
            Description = finalMessage,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.Add(delay)
            }
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private string GetSelectedAppsSummary()
    {
        var saved = Preferences.Default.Get("graveyard_apps", "");

        if (string.IsNullOrWhiteSpace(saved))
            return "Apps to avoid: none";

        try
        {
            var apps = System.Text.Json.JsonSerializer.Deserialize<List<The_Hunt_Khai_Tan_Sum.Models.AppItem>>(saved);

            if (apps == null)
                return "Apps to avoid: none";

            var selected = apps
                .Where(a => a.IsSelected)
                .Select(a => a.Name)
                .ToList();

            return selected.Count > 0
                ? $"Apps to avoid: {string.Join(", ", selected)}"
                : "Apps to avoid: none";
        }
        catch
        {
            return "Apps to avoid: none";
        }
    }

    public async Task ShowImmediateHuntNotification()
    { 
        var selectedAppsSummary = GetSelectedAppsSummary();
        var message = Preferences.Get("manifesto", "Stop. Is this distraction worth it");
        var mode = Preferences.Get("mode", "WHISPER");

        string title = mode == "POLTERGEIST" ? "⚠️ THE HUNT" : "👁️ Gentle Reminder";
        string finalMessage = mode == "POLTERGEIST"
            ? $"STOP.\n{message.ToUpper()}"
            : message;

        var request = new NotificationRequest
        {
            NotificationId = NotificationId,
            Title = title,
            Description = finalMessage
        };

        LocalNotificationCenter.Current.Show(request);

        if (mode == "POLTERGEIST")
        {
            try
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(300));
                await Task.Delay(200);

                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(500));
            }
            catch (Exception) { }
        }

        var now = DateTime.Now;
        string usageKey = $"usage_{now.Year}_{now.Month:D2}";
        int count = Preferences.Get(usageKey, 0);
        Preferences.Set(usageKey, count + 1);

        double totalMinutes = Preferences.Get("total_hunt_minutes", 0.0);
        Preferences.Set("total_hunt_minutes", totalMinutes + 1);
    }

    public void CancelHuntNotification()
    {
        LocalNotificationCenter.Current.Cancel(NotificationId);
    }
}