namespace SharePlay.Services
{
    using System;
    using System.Net;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayServerService : IPlayServerService
    {
        private readonly INetworkService _networkService;

        private readonly SimpleTcpServer _tcpServer = new SimpleTcpServer();

        private readonly ActionBroadcastingUtility _actionBroadcastingUtility;

        public PlayServerService(IMediaPlayerService mediaPlayerService, INetworkService networkService)
        {
            _networkService = networkService;
            _actionBroadcastingUtility = new ActionBroadcastingUtility(mediaPlayerService);
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public event EventHandler<ClientConnectedEventArgs> ClientDisconnected;

        public void Host(int port)
        {
            _tcpServer.Start(IPAddress.Any, port);

            _tcpServer.ClientConnected += (sender, e) => ClientConnected?.Invoke(this, new ClientConnectedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));
            _tcpServer.ClientDisconnected += (sender, e) => ClientDisconnected?.Invoke(this, new ClientConnectedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));

            _tcpServer.DataReceived += (sender, e) => _actionBroadcastingUtility.ReceiveAction(e.MessageString);

            _actionBroadcastingUtility.BroadcastAllActions(_tcpServer.Broadcast);
        }
    }
}