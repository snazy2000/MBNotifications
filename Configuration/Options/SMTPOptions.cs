using System;
using MBNotifications.Providers;

namespace MBNotifications.Configuration.Options
{
    public class SMTPOptions
    {
        public Boolean Enabled { get; set; }
        public String EmailFrom { get; set; }
        public String EmailTo { get; set; }
        public String Server { get; set; }
        public int Port { get; set; }
        public Boolean useCredentials { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public SMTPOptions()
        {
            Port = 25;
        }
    }
}
