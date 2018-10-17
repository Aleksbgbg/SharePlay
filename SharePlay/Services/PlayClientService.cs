namespace SharePlay.Services
{
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

        public void Connect(NetworkAddress networkAddress)
        {
            _tcpClient.Connect(networkAddress.IpAddress.ToString(), networkAddress.Port);
        }
    }
}