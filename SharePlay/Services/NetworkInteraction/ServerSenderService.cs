namespace SharePlay.Services.NetworkInteraction
{
    using System;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;

    using Caliburn.Micro;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    internal class ServerSenderService : IServerSenderService
    {
        private const double SyncIntervalMs = 2000.0;

        private readonly IMediaPlayerService _mediaPlayerService;

        private readonly Timer _syncTimer = new Timer(SyncIntervalMs)
        {
            AutoReset = true
        };

        private Action<string> _broadcastMethod;

        public ServerSenderService(IMediaPlayerService mediaPlayerService)
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
                Transmit(nameof(Progress), value);
                _mediaPlayerService.Progress = value;
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
                Transmit(nameof(Speed), value);
                _mediaPlayerService.Speed = value;
            }
        }

        public void Initialize(MediaElement mediaElement)
        {
            throw new NotSupportedException();
        }

        public void Initialize(Action<string> broadcastMethod)
        {
            _broadcastMethod = broadcastMethod;

            _syncTimer.Elapsed += async (sender, e) =>
            {
                TimeSpan progress = new TimeSpan();

                await Execute.OnUIThreadAsync(() => progress = _mediaPlayerService.Progress);

                Transmit(nameof(IClientReceiverService.Sync), progress);
            };

            if (IsPlaying)
            {
                _syncTimer.Start();
            }
        }

        public void Stop()
        {
            Transmit(nameof(Stop));
            _mediaPlayerService.Stop();

            _syncTimer.Stop();
        }

        public void Play()
        {
            Transmit(nameof(Play));
            _mediaPlayerService.Play();

            _syncTimer.Start();
        }

        public void Pause()
        {
            Transmit(nameof(Pause));
            _mediaPlayerService.Pause();

            _syncTimer.Stop();
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
            Transmit(nameof(Load), url);
            _mediaPlayerService.Load(url);

            _syncTimer.Start();
        }

        private void Transmit(string methodName, params object[] arguments)
        {
            _broadcastMethod(string.Join(" ", TimeUtility.TimeSinceSyncEpoch.Ticks, methodName, string.Join(" ", arguments)));
        }
    }
}