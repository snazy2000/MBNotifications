using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace MBNotifications.Providers
{
    class PushOver
    {
        private string url = "https://api.pushover.net/1/messages.json";
        private bool enabled = Plugin.Instance.Configuration.Notifications.PushOver.Enabled;
        private string userKey = Plugin.Instance.Configuration.Notifications.PushOver.UserKey;
        private string authToken = Plugin.Instance.Configuration.Notifications.PushOver.Token;
        private string deviceName = Plugin.Instance.Configuration.Notifications.PushOver.DeviceName;

        public async Task<bool> Push(string message)
        {
            if (enabled && !string.IsNullOrEmpty(userKey) && !string.IsNullOrEmpty(authToken))
            {
                var parameters = new NameValueCollection
                {
                    {"token", authToken},
                    {"user", userKey},
                    {"message", message}
                };

                if (string.IsNullOrEmpty(deviceName))
                {
                    parameters.Add("device", deviceName);
                }

                try
                {
                    using (var client = new WebClient())
                    {
                        client.UploadValues("https://api.pushover.net/1/messages.json", parameters);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Plugin.Logger.Error("MBNotifications - PushOver - " + ex.ToString());
                    return false;
                }
            }
            return false;
        }
    }
}
