namespace SharePlay.ViewModels
{
    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class PlayerViewModel : ViewModelBase, IPlayerViewModel
    {
        private readonly IMediaPlayerService _mediaPlayerService;

        public PlayerViewModel(IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;
        }

        public void Play(string videoUrl)
        {
            _mediaPlayerService.Load(videoUrl);
        }
    }
}