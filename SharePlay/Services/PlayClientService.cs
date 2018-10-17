namespace SharePlay.Services
{
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;

    using SimpleTCP;

    internal class PlayClientService : IPlayClientService
    {
        private readonly SimpleTcpClient _tcpClient = new SimpleTcpClient();

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

            mediaPlayerService.Played += (sender, e) => _tcpClient.Write("Play");
            mediaPlayerService.Paused += (sender, e) => _tcpClient.Write("Pause");
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