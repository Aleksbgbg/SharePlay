namespace SharePlay.Services
{
    using System;
    using System.Net;

    using SharePlay.EventArgs;
    using SharePlay.Factories.Interfaces;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayServerService : IPlayServerService
    {
        private readonly INetworkInteractionFactory _networkInteractionFactory;

        private readonly SimpleTcpServer _tcpServer = new SimpleTcpServer();

        private ActionBroadcastingUtility _actionBroadcastingUtility;

        public PlayServerService(INetworkInteractionFactory networkInteractionFactory, IServerSenderService serverSenderService)
        {
            _tcpServer.Delimiter = (byte)'\n';

            _networkInteractionFactory = networkInteractionFactory;

            serverSenderService.Initialize(_tcpServer.BroadcastLine);
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public event EventHandler<ClientConnectedEventArgs> ClientDisconnected;

        public void Host(int port)
        {
            _tcpServer.Start(IPAddress.Any, port);

            _actionBroadcastingUtility = new ActionBroadcastingUtility(_networkInteractionFactory.MakeServerReceiverService());

            _tcpServer.ClientConnected += (sender, e) => ClientConnected?.Invoke(this, new ClientConnectedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));
            _tcpServer.ClientDisconnected += (sender, e) => ClientDisconnected?.Invoke(this, new ClientConnectedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));

            _tcpServer.DelimiterDataReceived += (sender, e) => _actionBroadcastingUtility.ReceiveAction(e.MessageString);
        }
    }
}