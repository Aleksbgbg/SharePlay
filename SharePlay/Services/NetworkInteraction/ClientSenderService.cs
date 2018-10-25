namespace SharePlay.Services.NetworkInteraction
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class ClientSenderService : SenderService, IClientSenderService
    {
        private readonly IMediaPlayerService _mediaPlayerService;

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

                TransmitMemberCall(arguments: value);
            }
        }

        public TimeSpan Duration => _mediaPlayerService.Duration;

        public double Volume
        {
            get => _mediaPlayerService.Volume;

            set => _mediaPlayerService.Volume = value;
        }

        public double Speed
        {
            get => _mediaPlayerService.Speed;

            set
            {
                if (Speed == value) return;

                TransmitMemberCall(arguments: value);
            }
        }

        public void Initialize(MediaElement mediaElement)
        {
            throw new NotSupportedException();
        }

        public void Stop()
        {
            TransmitMemberCall();
        }

        public void Play()
        {
            TransmitMemberCall();
        }

        public void Pause()
        {
            TransmitMemberCall();
        }

        public void TogglePlay()
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        public void Load(string url)
        {
            TransmitMemberCall(arguments: url);
        }
    }
}