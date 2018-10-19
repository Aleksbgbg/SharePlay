namespace SharePlay.Services
{
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using SharePlay.Factories.Interfaces;
    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayClientService : IPlayClientService
    {
        private readonly INetworkInteractionFactory _networkInteractionFactory;

        private readonly SimpleTcpClient _tcpClient = new SimpleTcpClient();

        private ActionBroadcastingUtility _actionBroadcastingUtility;

        public PlayClientService(INetworkInteractionFactory networkInteractionFactory, IClientSenderService clientSenderService)
        {
            _tcpClient.Delimiter = (byte)'\n';

            _networkInteractionFactory = networkInteractionFactory;

            clientSenderService.Initialize(_tcpClient.WriteLine);
        }

        public async Task<bool> TryConnect(NetworkAddress networkAddress)
        {
            bool didConnect = await Task.Run(() =>
            {
                try
                {
                    _tcpClient.Connect(networkAddress.IpAddress.ToString(), networkAddress.Port);
                    return true;
                }
                catch (SocketException)
                {
                    return false;
                }
            });

            if (didConnect)
            {
                _actionBroadcastingUtility = new ActionBroadcastingUtility(_networkInteractionFactory.MakeClientReceiverService());
                _tcpClient.DelimiterDataReceived += (sender, e) => _actionBroadcastingUtility.ReceiveAction(e.MessageString);
            }

            return didConnect;
        }
    }
}