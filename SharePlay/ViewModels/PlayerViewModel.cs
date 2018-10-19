namespace SharePlay.ViewModels
{
    using System;

    using Caliburn.Micro;

    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class PlayerViewModel : ViewModelBase, IPlayerViewModel, IHandle<IMediaPlayerService>
    {
        private IMediaPlayerService _mediaPlayerService;

        private bool _syncPlayer = true;

        public PlayerViewModel(IEventAggregator eventAggregator, IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;

            eventAggregator.Subscribe(this);
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

        public double Speed
        {
            get => _mediaPlayerService.Speed;

            set
            {
                _mediaPlayerService.Speed = value;
                NotifyOfPropertyChange(() => Speed);
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

        public void Load(string videoUrl)
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

        public void Handle(IMediaPlayerService message)
        {
            // Swaps out the current service for a network transmitter
            // dummy service once a server has been hosted or joined
            _mediaPlayerService = message;
        }
    }
}