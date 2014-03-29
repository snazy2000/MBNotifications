using System;

namespace MBNotifications.Configuration.Options
{
    public class PushOverOptions
    {
        public Boolean Enabled { get; set; }
        public String UserKey { get; set; }
        public String Token { get; set; }
        public String DeviceName { get; set; }
    }
}
