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

                if (!string.IsNullOrEmpty(deviceName))
                {
                    Plugin.Logger.Debug("MBNotifications - PushOver - Device name - " + deviceName);
                    parameters.Add("device", deviceName);
                }

                try
                {
                    Plugin.Logger.Debug("MBNotifications - PushOver - Token - " + authToken + " - User key - " + userKey );
                    using (var client = new WebClient())
                    {
                        client.UploadValues(url, parameters);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    var wex = (WebException)ex;
                    var s = wex.Response.GetResponseStream();
                    string ss = "";
                    int lastNum = 0;
                    do
                    {
                        lastNum = s.ReadByte();
                        ss += (char)lastNum;
                    } while (lastNum != -1);
                    s.Close();

                    Plugin.Logger.Error("MBNotifications - PushOver - " + ss);
                    return false;
                }
            }
            return false;
        }
    }
}
