using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MBNotifications.Providers
{
    class SMTP
    {
        private readonly bool enabled = Plugin.Instance.Configuration.Notifications.PushOver.Enabled;
        private readonly string emailFrom = Plugin.Instance.Configuration.Notifications.SMTP.EmailFrom;
        private readonly string emailTo = Plugin.Instance.Configuration.Notifications.SMTP.EmailTo;
        private readonly string server = Plugin.Instance.Configuration.Notifications.SMTP.Server;
        private readonly int port = Plugin.Instance.Configuration.Notifications.SMTP.Port;
        private readonly bool useCredentials = Plugin.Instance.Configuration.Notifications.SMTP.useCredentials;
        private readonly string username = Plugin.Instance.Configuration.Notifications.SMTP.Username;
        private readonly string password = Plugin.Instance.Configuration.Notifications.SMTP.Password;

        public async Task<bool> Push(string message)
        {
            if (enabled &&
                !string.IsNullOrEmpty(emailFrom) &&
                !string.IsNullOrEmpty(emailTo) &&
                !string.IsNullOrEmpty(server))
            {
                var mail = new MailMessage(emailFrom, emailTo)
                {
                    Subject = "Email From Your Media Server",
                    Body = message
                };

                var client = new SmtpClient
                {
                    Host = server,
                    Port = port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                if (useCredentials) client.Credentials = new NetworkCredential(username, password);

                try
                {
                    client.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    Plugin.Logger.Error("MBNotifications - SMTP - " + ex.ToString());
                    return false;
                }              
            }
            return false;
        }
    }
}
