namespace SharePlay
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

    using SharePlay.Factories;
    using SharePlay.Factories.Interfaces;
    using SharePlay.Services;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.ViewModels;
    using SharePlay.ViewModels.Interfaces;

    internal class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        internal AppBootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            // Register Factories
            _container.Singleton<IConnectionFactory, ConnectionFactory>();
            _container.Singleton<INetworkFactory, NetworkFactory>();

            // Register Services
            _container.Singleton<IWindowManager, WindowManager>();

            _container.Singleton<IConnectionWindowService, ConnectionWindowService>();

            _container.Singleton<IMediaPlayerService, MediaPlayerService>();
            _container.Singleton<INetworkService, NetworkService>();

            _container.Singleton<IPlayServerService, PlayServerService>();
            _container.Singleton<IPlayClientService, PlayClientService>();

            _container.Singleton<IClientSenderService, ClientSenderService>();

            // Register ViewModels
            _container.Singleton<IShellViewModel, ShellViewModel>();
            _container.Singleton<IMainViewModel, MainViewModel>();
            _container.Singleton<IPlayerViewModel, PlayerViewModel>();

            _container.Singleton<IHostServerViewModel, HostServerViewModel>();
            _container.Singleton<IJoinServerViewModel, JoinServerViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
        }
    }
}