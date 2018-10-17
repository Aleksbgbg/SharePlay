namespace SharePlay.Services
{
    using System;
    using System.Net;

    using Caliburn.Micro;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;

    using SimpleTCP;

    internal class PlayServerService : IPlayServerService
    {
        private readonly INetworkService _networkService;

        private readonly SimpleTcpServer _tcpServer = new SimpleTcpServer();

        public PlayServerService(IMediaPlayerService mediaPlayerService, INetworkService networkService)
        {
            _networkService = networkService;

            _tcpServer.ClientConnected += (sender, e) => ClientConnected?.Invoke(this, new ClientConnectedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));
            _tcpServer.ClientDisconnected += (sender, e) => ClientDisconnected?.Invoke(this, new ClientConnectedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));

            _tcpServer.DataReceived += (sender, e) => Execute.OnUIThread(() =>
            {
                if (e.MessageString == "Play")
                {
                    mediaPlayerService.Play();
                }
                else
                {
                    mediaPlayerService.Pause();
                }
            });

            mediaPlayerService.Played += (sender, e) => _tcpServer.Broadcast("Play");
            mediaPlayerService.Paused += (sender, e) => _tcpServer.Broadcast("Pause");
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public event EventHandler<ClientConnectedEventArgs> ClientDisconnected;

        public void Host(int port)
        {
            _tcpServer.Start(port);
        }
    }
}