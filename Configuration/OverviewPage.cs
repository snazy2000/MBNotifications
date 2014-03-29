using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using System;
using System.IO;

namespace MBNotifications.Configuration
{
    /// <summary>
    /// Class MyConfigurationPage
    /// </summary>
    class OverviewPage : IPluginConfigurationPage
    {
        /// <summary>
        /// Gets My Option.
        /// </summary>
        /// <value>The Option.</value>
        public string Name
        {
            get { return "OverView"; }
        }

        /// <summary>
        /// Gets the HTML stream.
        /// </summary>
        /// <returns>Stream.</returns>
        public Stream GetHtmlStream()
        {
            return GetType().Assembly.GetManifestResourceStream("MBNotifications.Configuration.overviewPage.html");
        }

        /// <summary>
        /// Gets the type of the configuration page.
        /// </summary>
        /// <value>The type of the configuration page.</value>
        public ConfigurationPageType ConfigurationPageType
        {
            get { return ConfigurationPageType.PluginConfiguration; }
        }

        public IPlugin Plugin
        {
            get { return MBNotifications.Plugin.Instance; }
        }
    }
}
