﻿namespace SharePlay.ViewModels
{
    using System.Net;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class HostServerViewModel : ViewModelBase, IHostServerViewModel
    {
        public HostServerViewModel(INetworkService networkService)
        {
            Task.Run(async () => await networkService.ConfigureMachineForHosting().ContinueWith(task =>
            {
                HostAddress = new NetworkAddress(networkService.ExternalIp, 3555);
                IsLoading = false;
            }));
        }

        public NetworkAddress HostAddress { get; private set; }

        public IObservableCollection<IPAddress> ConnectedUsers { get; } = new BindableCollection<IPAddress>();

        private bool _isIsLoading = true;
        public bool IsLoading
        {
            get => _isIsLoading;

            private set
            {
                if (_isIsLoading == value) return;

                _isIsLoading = value;
                NotifyOfPropertyChange(() => IsLoading);
            }
        }
    }
}