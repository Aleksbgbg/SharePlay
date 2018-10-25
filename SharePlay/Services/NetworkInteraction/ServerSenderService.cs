namespace SharePlay.Services.NetworkInteraction
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;

    using Caliburn.Micro;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    internal class ServerSenderService : SenderService, IServerSenderService
    {
        private const double SyncIntervalMs = 2000.0;

        private readonly IMediaPlayerService _mediaPlayerService;

        private readonly Timer _syncTimer = new Timer(SyncIntervalMs)
        {
            AutoReset = true
        };

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
                // TODO: Ensure that these value checks are worth the effort
                if (Progress == value) return;

                TransmitMemberCall(arguments: value);
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
                // TODO: Ensure that these value checks are worth the effort
                if (Speed == value) return;

                TransmitMemberCall(arguments: value);
                _mediaPlayerService.Speed = value;
            }
        }

        public void Initialize(MediaElement mediaElement)
        {
            throw new NotSupportedException();
        }

        public void Stop()
        {
            TransmitMemberCall();

            _mediaPlayerService.Stop();
            _syncTimer.Stop();
        }

        public void Play()
        {
            TransmitMemberCall();

            _mediaPlayerService.Play();
            _syncTimer.Start();
        }

        public void Pause()
        {
            TransmitMemberCall();

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
            TransmitMemberCall(arguments: url);

            _mediaPlayerService.Load(url);
        }

        private protected override void OnInitialize()
        {
            _syncTimer.Elapsed += async (sender, e) =>
            {
                TimeSpan progress = new TimeSpan();

                await Execute.OnUIThreadAsync(() => progress = _mediaPlayerService.Progress);

                TransmitMemberCall(nameof(IClientReceiverService.Sync), progress);
            };

            if (IsPlaying)
            {
                _syncTimer.Start();
            }
        }

        private protected override void InsertArgumentsBeforeTransmission(List<string> transmissionArguments)
        {
            transmissionArguments.Insert(0, TimeUtility.TimeSinceSyncEpoch.Ticks.ToString());
        }
    }
}