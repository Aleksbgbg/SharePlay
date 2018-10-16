namespace SharePlay.ViewModels
{
    using System.Net;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.ViewModels.Interfaces;

    internal sealed class HostServerViewModel : ViewModelBase, IHostServerViewModel
    {
        public HostServerViewModel()
        {
            DisplayName = "Host Server";
        }

        public NetworkAddress HostAddress { get; }

        public IObservableCollection<IPAddress> ConnectedUsers { get; } = new BindableCollection<IPAddress>();
    }
}