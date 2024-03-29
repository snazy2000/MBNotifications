﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBNotifications.Configuration;
using MediaBrowser.Common;
using MediaBrowser.Common.Events;
using MediaBrowser.Common.ScheduledTasks;
using MediaBrowser.Common.Security;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Controller.Session;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MBNotifications.Providers;
using MediaBrowser.Model.Tasks;

namespace MBNotifications
{
    /// <summary>
    /// Class ServerEntryPoint
    /// </summary>
    public class ServerEntryPoint : IServerEntryPoint, IRequiresRegistration
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ServerEntryPoint Instance { get; private set; }

        /// <summary>
        /// Access to the LibraryManager of MB Server
        /// </summary>
        public ILibraryManager _libraryManager { get; private set; }

        /// <summary>
        /// Access to the SecurityManager of MB Server
        /// </summary>
        public ISecurityManager _securityManager { get; set; }

        public ISessionManager _sessionManager { get; set; }

        public IApplicationHost _apphost { get; set; }

        public ITaskManager _taskManager { get; set; }

        Pusher _pusher { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerEntryPoint" /> class.
        /// </summary>
        /// <param name="taskManager">The task manager.</param>
        /// <param name="appPaths">The app paths.</param>
        /// <param name="logManager"></param>
        public ServerEntryPoint(ISessionManager sessionManager,  ILibraryManager libraryManager, IApplicationHost apphost, ILogManager logManager, ISecurityManager securityManager, ITaskManager taskManager)
        {
            _sessionManager = sessionManager;
            _libraryManager = libraryManager;
            _apphost = apphost;
            _taskManager = taskManager;
            _securityManager = securityManager;
            _pusher = new Pusher();
            
            Plugin.Logger = logManager.GetLogger(Plugin.Instance.Name);
          
            Instance = this;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            _libraryManager.ItemAdded += LibraryManagerItemAdded;
            _libraryManager.ItemRemoved += LibraryManagerItemRemoved;
            _sessionManager.PlaybackStart += PlaybackStart;
            _apphost.HasPendingRestartChanged += _apphost_HasPendingRestartChanged;
            _taskManager.TaskCompleted += _taskManager_TaskCompleted;
        }

        void _taskManager_TaskCompleted(object sender, GenericEventArgs<TaskResult> e)
        {
            if (Plugin.Instance.Configuration.Notifications.Tasks)
            {
                if (e.Argument.Status.ToString() == "Failed")
                {
                    _pusher.Push("Media Server Task " + e.Argument.Name + " Error : " + e.Argument.ErrorMessage, 0);
                }
            }
        }

        void _apphost_HasPendingRestartChanged(object sender, System.EventArgs e)
        {
            Plugin.Logger.Debug("Notifications - System");

            if (Plugin.Instance.Configuration.Notifications.System)
            {
                _pusher.Push("Your Media Server Needs a Restart To Apply An update", 0);
            }
        }

        private void PlaybackStart(object sender, PlaybackProgressEventArgs e)
        {
            Plugin.Logger.Debug("Notifications - PlayBack");
                    
            if (Plugin.Instance.Configuration.Notifications.PlayBack)
            {
                if (e.Users == null || !e.Users.Any() || e.Item == null)
                {
                    Plugin.Logger.Error("Notifications - Event details incomplete. Cannot process current media");
                    return;
                }
                _pusher.Push(e.Users.FirstOrDefault() + " is watching " + e.Item, 0);
            }
        }

        private void LibraryManagerItemRemoved(object sender, ItemChangeEventArgs e)
        {
            Plugin.Logger.Debug("Notifications - Removed");

            if (Plugin.Instance.Configuration.Notifications.Libray)
            {
                if (e.Item.LocationType == LocationType.Virtual) return;
                _pusher.Push(e.Item + " has been removed to your media server.", 0);
            }
        }

        private void LibraryManagerItemAdded(object sender, ItemChangeEventArgs e)
        {
            Plugin.Logger.Debug("Notifications - Added");

            if (Plugin.Instance.Configuration.Notifications.Libray)
            {
                if (e.Item.LocationType == LocationType.Virtual) return;

                _pusher.Push(e.Item + " has been added to your media server.", 0);
            }
        }

        /// <summary>
        /// Called when [configuration updated].
        /// </summary>
        /// <param name="oldConfig">The old config.</param>
        /// <param name="newConfig">The new config.</param>
        public void OnConfigurationUpdated(PluginConfiguration oldConfig, PluginConfiguration newConfig)
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Loads our registration information
        ///
        /// </summary>
        /// <returns></returns>
        public async Task LoadRegistrationInfoAsync()
        {
            Plugin.Instance.Registration = await _securityManager.GetRegistrationStatus("MBNotifications").ConfigureAwait(false);
            Plugin.Logger.Debug("MBNotifications Registration Status - Registered: {0} In trial: {2} Expiration Date: {1} Is Valid: {3}", Plugin.Instance.Registration.IsRegistered, Plugin.Instance.Registration.ExpirationDate, Plugin.Instance.Registration.TrialVersion, Plugin.Instance.Registration.IsValid);
        }

    }
}
