using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using System.IO;

namespace MBNotifications.Configuration
{
    /// <summary>
    /// Class MyConfigurationPage
    /// </summary>
    class SMTPPage : IPluginConfigurationPage
    {
        /// <summary>
        /// Gets My Option.
        /// </summary>
        /// <value>The Option.</value>
        public string Name
        {
            get { return "SMTPOptions"; }
        }

        /// <summary>
        /// Gets the HTML stream.
        /// </summary>
        /// <returns>Stream.</returns>
        public Stream GetHtmlStream()
        {
            return GetType().Assembly.GetManifestResourceStream("MBNotifications.Configuration.SMTPPage.html");
        }

        /// <summary>
        /// Gets the type of the configuration page.
        /// </summary>
        /// <value>The type of the configuration page.</value>
        public ConfigurationPageType ConfigurationPageType
        {
            get { return ConfigurationPageType.None; }
        }

        public IPlugin Plugin
        {
            get { return MBNotifications.Plugin.Instance; }
        }
    }
}
