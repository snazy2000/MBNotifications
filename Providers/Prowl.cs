using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace MBNotifications.Providers
{
    class Prowl
    {
        private string url = "https://api.prowlapp.com/publicapi/add";
        private bool enabled = Plugin.Instance.Configuration.Notifications.PushALot.Enabled;
        private string authToken = Plugin.Instance.Configuration.Notifications.PushALot.Token;
        
        public async Task<bool> Push(string message)
        {
            if (enabled && !string.IsNullOrEmpty(authToken))
            {
                var parameters = new NameValueCollection
                {
                    {"apikey ", authToken},
                    {"description ", message}
                };

                try
                {
                    Plugin.Logger.Debug("MBNotifications - Prowl - Token - " + authToken );
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

                    Plugin.Logger.Error("MBNotifications - Prowl - " + ss);
                    return false;
                }
            }
            return false;
        }
    }
}
