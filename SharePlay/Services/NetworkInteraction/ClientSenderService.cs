namespace SharePlay.Services.NetworkInteraction
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class ClientSenderService : IClientSenderService
    {
        private readonly IMediaPlayerService _mediaPlayerService;

        private Action<string> _broadcastMethod;

        public ClientSenderService(IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;
        }

        public event RoutedEventHandler MediaOpened
        {
            add => _mediaPlayerService.MediaOpened += value;

            remove => _mediaPlayerService.MediaOpened -= value;
        }

        public event RoutedEventHandler MediaEnded
        {
            add => _mediaPlayerService.MediaEnded += value;

            remove => _mediaPlayerService.MediaEnded -= value;
        }

        public event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated
        {
            add => _mediaPlayerService.ProgressUpdated += value;

            remove => _mediaPlayerService.ProgressUpdated -= value;
        }

        public event EventHandler Paused
        {
            add => _mediaPlayerService.Paused += value;

            remove => _mediaPlayerService.Paused -= value;
        }

        public event EventHandler Played
        {
            add => _mediaPlayerService.Played += value;

            remove => _mediaPlayerService.Played -= value;
        }

        public bool IsPlaying => _mediaPlayerService.IsPlaying;

        public TimeSpan Progress
        {
            get => _mediaPlayerService.Progress;

            set
            {
                if (Progress == value) return;

                _broadcastMethod(string.Concat(nameof(Progress), " ", value));
            }
        }

        public TimeSpan Duration => _mediaPlayerService.Duration;

        public double Volume
        {
            get => _mediaPlayerService.Volume;

            set
            {
                if (Volume == value) return;

                _broadcastMethod(string.Concat(nameof(Volume), " ", value));
            }
        }

        public double Speed
        {
            get => _mediaPlayerService.Speed;

            set
            {
                if (Speed == value) return;

                _broadcastMethod(string.Concat(nameof(Speed), " ", value));
            }
        }

        public void Initialize(MediaElement mediaElement)
        {
            throw new NotSupportedException();
        }

        public void Initialize(Action<string> broadcastMethod)
        {
            _broadcastMethod = broadcastMethod;
        }

        public void Stop()
        {
            _broadcastMethod(nameof(Stop));
        }

        public void Play()
        {
            _broadcastMethod(nameof(Play));
        }

        public void Pause()
        {
            _broadcastMethod(nameof(Pause));
        }

        public void TogglePlay()
        {
            if (IsPlaying)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }

        public void Load(string url)
        {
            _broadcastMethod(string.Concat(nameof(Load), " ", url));
        }
    }
}