using System;
using MBNotifications.Configuration.Options;

namespace MBNotifications.Configuration
{
    public class NotificationsOptions
    {
        public PushOverOptions PushOver { get; set; }
        public SMTPOptions SMTP { get; set; }


        public Boolean PlayBack { get; set; }
        public Boolean Libray { get; set; }
        public Boolean System { get; set; }

        public NotificationsOptions()
        {
            PushOver = new PushOverOptions();
            SMTP = new SMTPOptions();
        }
    }

}
