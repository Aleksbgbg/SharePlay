namespace SharePlay.Services
{
    using System;
    using System.Net;

    using Caliburn.Micro;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayServerService : IPlayServerService
    {
        private readonly INetworkService _networkService;

        private readonly SimpleTcpServer _tcpServer = new SimpleTcpServer();

        private readonly ActionBroadcastingUtility _actionBroadcastingUtility = new ActionBroadcastingUtility();

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

            _actionBroadcastingUtility.BroadcastActions(mediaPlayerService, _tcpServer.Broadcast);
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public event EventHandler<ClientConnectedEventArgs> ClientDisconnected;

        public void Host(int port)
        {
            _tcpServer.Start(port);
        }
    }
}