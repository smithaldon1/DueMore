using System;
using System.Collections.Generic;
using System.Text;

namespace DueMore.Models
{
    public enum NotifMethod
    {
        Banner,
        LockScreen,
        NotificationCenter,
        PushNotifications,
        Badges
    }

    public class ItemStyle
    {
        public NotifMethod Method { get; set; }
        public bool NotifEnable { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
    }
}
