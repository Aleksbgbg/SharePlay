namespace SharePlay.Services.Interfaces
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using SharePlay.EventArgs;

    internal interface IMediaPlayerService
    {
        event RoutedEventHandler MediaOpened;

        event RoutedEventHandler MediaEnded;

        event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        event EventHandler Paused;

        event EventHandler Played;

        bool IsPlaying { get; }

        TimeSpan Progress { get; set; }

        TimeSpan Duration { get; }

        double Volume { get; set; }

        void Initialize(MediaElement mediaElement);

        void Stop();

        void Play();

        void Pause();

        void TogglePlay();

        void Load(string url);
    }
}