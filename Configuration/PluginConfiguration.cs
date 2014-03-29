using MediaBrowser.Model.Plugins;

namespace MBNotifications.Configuration
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public NotificationsOptions Notifications { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginConfiguration" /> class.
        /// </summary>
        public PluginConfiguration()
        {
            Notifications = new NotificationsOptions();
        }
    }
}
