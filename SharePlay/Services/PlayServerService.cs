namespace SharePlay.Services
{
    using Caliburn.Micro;

    using SharePlay.Services.Interfaces;

    using SimpleTCP;

    internal class PlayServerService : IPlayServerService
    {
        private readonly INetworkService _networkService;

        private readonly SimpleTcpServer _tcpServer = new SimpleTcpServer().Start(3555);

        public PlayServerService(IMediaPlayerService mediaPlayerService, INetworkService networkService)
        {
            _networkService = networkService;

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
    }
}