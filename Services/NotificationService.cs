using System;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace Services
{
    public class NotificationService
    {
        public void ScheduleHuntNotification(TimeSpan delay)
        {
            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "The Hunt is Starting",
                Description = "Your delay timer had finished. Time to focus.",
                Schedule =
                {
                    NotifyTime = DateTime.Now.Add(delay)
                    // this shedukes the native alert for the future
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }
    }
}