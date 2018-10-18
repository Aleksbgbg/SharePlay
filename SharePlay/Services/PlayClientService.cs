namespace SharePlay.Services
{
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayClientService : IPlayClientService
    {
        private readonly SimpleTcpClient _tcpClient = new SimpleTcpClient();

        private readonly ActionBroadcastingUtility _actionBroadcastingUtility;

        public PlayClientService(IMediaPlayerService mediaPlayerService)
        {
            _actionBroadcastingUtility = new ActionBroadcastingUtility(mediaPlayerService);
        }

        public async Task<bool> TryConnect(NetworkAddress networkAddress)
        {
            bool result = await Task.Run(() =>
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

            if (result)
            {
                _tcpClient.DataReceived += (sender, e) => _actionBroadcastingUtility.ReceiveAction(e.MessageString);

                _actionBroadcastingUtility.BroadcastAllActions(_tcpClient.Write);
            }

            return result;
        }
    }
}