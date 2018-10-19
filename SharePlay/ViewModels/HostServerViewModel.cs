namespace SharePlay.ViewModels
{
    using System.Net;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class HostServerViewModel : ViewModelBase, IHostServerViewModel
    {
        private readonly INetworkService _networkService;

        private readonly IPlayServerService _playServerService;

        public HostServerViewModel(INetworkService networkService, IPlayServerService playServerService)
        {
            _networkService = networkService;
            _playServerService = playServerService;

            playServerService.ClientConnected += (sender, e) => ConnectedUsers.Add(e.Address);
            playServerService.ClientDisconnected += (sender, e) => ConnectedUsers.Remove(e.Address);

            Task.Run(async () => await networkService.ConfigureMachineForHosting().ContinueWith(task =>
            {
                HostAddress = new NetworkAddress(networkService.ExternalIp, networkService.Port);
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

        public void Host()
        {
            _playServerService.Host(_networkService.Port);
        }
    }
}