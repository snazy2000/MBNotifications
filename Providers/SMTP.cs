using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MBNotifications.Providers
{
    class SMTP
    {
        private bool enabled = Plugin.Instance.Configuration.Notifications.PushOver.Enabled;
        private string emailFrom = Plugin.Instance.Configuration.Notifications.SMTP.EmailFrom;
        private string emailTo = Plugin.Instance.Configuration.Notifications.SMTP.EmailTo;
        private string server = Plugin.Instance.Configuration.Notifications.SMTP.Server;
        private int port = Plugin.Instance.Configuration.Notifications.SMTP.Port;
        private bool useCredentials = Plugin.Instance.Configuration.Notifications.SMTP.useCredentials;
        private string username = Plugin.Instance.Configuration.Notifications.SMTP.Username;
        private string password = Plugin.Instance.Configuration.Notifications.SMTP.Password;

        public async Task<bool> Push(string message)
        {
            if (enabled &&
                !string.IsNullOrEmpty(emailFrom) &&
                !string.IsNullOrEmpty(emailTo) &&
                !string.IsNullOrEmpty(server))
            {
                MailMessage mail = new MailMessage(emailFrom, emailTo);
                SmtpClient client = new SmtpClient();
                client.Host = server;
                client.Port = port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                if (useCredentials)
                {
                    client.Credentials = new NetworkCredential(username, password);
                }

                mail.Subject = "Email From Your Media Server";
                mail.Body = message;

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
