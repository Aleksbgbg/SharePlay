namespace SharePlay.Services
{
    using Caliburn.Micro;

    using SharePlay.Services.Interfaces;

    using SimpleTCP;

    internal class PlayClientService : IPlayClientService
    {
        private readonly SimpleTcpClient _tcpClient = new SimpleTcpClient().Connect("localhost", 3555);

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
    }
}