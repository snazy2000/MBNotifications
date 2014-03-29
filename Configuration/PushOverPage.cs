using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using System;
using System.IO;

namespace MBNotifications.Configuration
{
    /// <summary>
    /// Class MyConfigurationPage
    /// </summary>
    class PushOverPage : IPluginConfigurationPage
    {
        /// <summary>
        /// Gets My Option.
        /// </summary>
        /// <value>The Option.</value>
        public string Name
        {
            get { return "PushOverOptions"; }
        }

        /// <summary>
        /// Gets the HTML stream.
        /// </summary>
        /// <returns>Stream.</returns>
        public Stream GetHtmlStream()
        {
            return GetType().Assembly.GetManifestResourceStream("MBNotifications.Configuration.pushOverPage.html");
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
