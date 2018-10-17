namespace SharePlay.ViewModels
{
    using System.Net;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.ViewModels.Interfaces;

    internal class HostServerViewModel : ViewModelBase, IHostServerViewModel
    {
        public NetworkAddress HostAddress { get; }

        public IObservableCollection<IPAddress> ConnectedUsers { get; } = new BindableCollection<IPAddress>();
    }
}