namespace SharePlay.Services
{
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayClientService : IPlayClientService
    {
        private readonly SimpleTcpClient _tcpClient = new SimpleTcpClient();

        private readonly ActionBroadcastingUtility _actionBroadcastingUtility = new ActionBroadcastingUtility();

        public PlayClientService(IMediaPlayerService mediaPlayerService)
        {
            _tcpClient.DataReceived += (sender, e) => Execute.OnUIThread(() =>
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

            _actionBroadcastingUtility.BroadcastActions(mediaPlayerService, _tcpClient.Write);
        }

        public async Task<bool> TryConnect(NetworkAddress networkAddress)
        {
            return await Task.Run(() =>
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
        }
    }
}