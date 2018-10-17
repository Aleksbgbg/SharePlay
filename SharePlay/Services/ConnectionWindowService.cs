namespace SharePlay.Services
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using SharePlay.Factories.Interfaces;
    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class ConnectionWindowService : IConnectionWindowService
    {
        private static readonly Dictionary<string, object> WindowSettings = new Dictionary<string, object>
        {
            ["MinWidth"] = 400,
            ["MinHeight"] = 500,
            ["ShowInTaskbar"] = false
        };

        private readonly IConnectionFactory _connectionFactory;

        private readonly IWindowManager _windowManager;

        public ConnectionWindowService(IConnectionFactory connectionFactory, IWindowManager windowManager)
        {
            _connectionFactory = connectionFactory;
            _windowManager = windowManager;
        }

        public IHostServerViewModel ShowHostWindow()
        {
            return ShowConnectWindow(_connectionFactory.MakeHostServerViewModel, "Host Server");
        }

        public IJoinServerViewModel ShowJoinWindow()
        {
            return ShowConnectWindow(_connectionFactory.MakeJoinServerViewModel, "Join Server");
        }

        private T ShowConnectWindow<T>(Func<T> windowFactory, string title)
        {
            T connectionWindowViewModel = windowFactory();

            Dictionary<string, object> settings = new Dictionary<string, object>(WindowSettings)
            {
                ["Title"] = title
            };

            _windowManager.ShowWindow(connectionWindowViewModel, settings: settings);

            return connectionWindowViewModel;
        }
    }
}