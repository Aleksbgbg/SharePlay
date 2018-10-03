namespace SharePlay.ViewModels
{
    using System;

    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class PlayerViewModel : ViewModelBase, IPlayerViewModel
    {
        private bool _syncPlayer = true;

        private readonly IMediaPlayerService _mediaPlayerService;

        public PlayerViewModel(IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;
        }

        public bool IsPlaying => _mediaPlayerService.IsPlaying;

        private TimeSpan _currentProgress;
        public TimeSpan CurrentProgress
        {
            get => _currentProgress;

            set
            {
                if (_currentProgress == value) return;

                _currentProgress = value;
                NotifyOfPropertyChange(() => CurrentProgress);

                if (_syncPlayer)
                {
                    _mediaPlayerService.Progress = _currentProgress;
                }
            }
        }

        public TimeSpan Duration => _mediaPlayerService.Duration;

        public double Volume
        {
            get => _mediaPlayerService.Volume;

            set
            {
                _mediaPlayerService.Volume = value;
                NotifyOfPropertyChange(() => Volume);
            }
        }

        public void TogglePlay()
        {
            _mediaPlayerService.TogglePlay();
        }

        public void DragStarted()
        {
            _syncPlayer = false;
        }

        public void DragCompleted()
        {
            _mediaPlayerService.Progress = CurrentProgress;
            _syncPlayer = true;
        }

        public void Play(string videoUrl)
        {
            _mediaPlayerService.Load(videoUrl);
        }

        protected override void OnViewAttached(object view, object context)
        {
            _mediaPlayerService.MediaOpened += (sender, e) => NotifyOfPropertyChange(() => Duration);
            _mediaPlayerService.Played += (sender, e) => NotifyOfPropertyChange(() => IsPlaying);
            _mediaPlayerService.Paused += (sender, e) => NotifyOfPropertyChange(() => IsPlaying);
            _mediaPlayerService.ProgressUpdated += (sender, e) =>
            {
                if (_syncPlayer)
                {
                    _syncPlayer = false;
                    CurrentProgress = e.NewProgress;
                    _syncPlayer = true;
                }
            };
        }
    }
}