namespace SharePlay.Services
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;

    internal class MediaPlayerService : IMediaPlayerService
    {
        private MediaElement _mediaElement;

        private readonly DispatcherTimer _progressUpdateTimer;

        public MediaPlayerService()
        {
            _progressUpdateTimer = new DispatcherTimer(TimeSpan.FromSeconds(0.1),
                                                       DispatcherPriority.Render,
                                                       (sender, e) => ProgressUpdated?.Invoke(this, new ProgressUpdatedEventArgs(Progress)),
                                                       Dispatcher.CurrentDispatcher);
        }

        public event RoutedEventHandler MediaOpened
        {
            add => _mediaElement.MediaOpened += value;

            remove => _mediaElement.MediaOpened -= value;
        }

        public event RoutedEventHandler MediaEnded
        {
            add => _mediaElement.MediaEnded += value;

            remove => _mediaElement.MediaEnded -= value;
        }

        public event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        public event EventHandler Paused;

        public event EventHandler Played;

        public bool IsPlaying { get; private set; }

        public TimeSpan Progress
        {
            get => _mediaElement.Position;

            set => _mediaElement.Position = value;
        }

        public TimeSpan Duration => _mediaElement.NaturalDuration.HasTimeSpan ? _mediaElement.NaturalDuration.TimeSpan : new TimeSpan();

        public double Volume
        {
            get => _mediaElement.Volume;

            set => _mediaElement.Volume = value;
        }

        public double Speed
        {
            get => _mediaElement.SpeedRatio;

            set => _mediaElement.SpeedRatio = value;
        }

        public void Initialize(MediaElement mediaElement)
        {
            _mediaElement = mediaElement;

            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.IsMuted = false;
        }

        public void Stop()
        {
            _mediaElement.Stop();
            IsPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
            _progressUpdateTimer.Stop();
        }

        public void Play()
        {
            _mediaElement.Play();
            IsPlaying = true;
            Played?.Invoke(this, EventArgs.Empty);
            _progressUpdateTimer.Start();
        }

        public void Pause()
        {
            _mediaElement.Pause();
            IsPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
            _progressUpdateTimer.Stop();
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
            string encodedUrl = WebUtility.UrlEncode(url);

            _mediaElement.Source = new Uri($"http://localhost:3000/stream/{encodedUrl}");
            Play();
        }
    }
}