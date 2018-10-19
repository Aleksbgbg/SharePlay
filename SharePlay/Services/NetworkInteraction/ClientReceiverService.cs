namespace SharePlay.Services.NetworkInteraction
{
    using System;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class ClientReceiverService : IClientReceiverService
    {
        private readonly IMediaPlayerService _mediaPlayerService;

        public ClientReceiverService(IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;
        }

        public MessageContext MessageContext { private get; set; }

        public void Progress(TimeSpan value)
        {
            _mediaPlayerService.Progress = value + MessageContext.MessageDelay;
        }

        public void Volume(double value)
        {
            _mediaPlayerService.Volume = value;
        }

        public void Speed(double value)
        {
            _mediaPlayerService.Progress += TimeSpan.FromSeconds((value - _mediaPlayerService.Speed) * MessageContext.MessageDelay.TotalSeconds);
            _mediaPlayerService.Speed = value;
        }

        public void Stop()
        {
            _mediaPlayerService.Stop();
            _mediaPlayerService.Progress -= MessageContext.MessageDelay;
        }

        public void Play()
        {
            _mediaPlayerService.Play();
            _mediaPlayerService.Progress += MessageContext.MessageDelay;
        }

        public void Pause()
        {
            _mediaPlayerService.Pause();
            _mediaPlayerService.Progress -= MessageContext.MessageDelay;
        }

        public void Load(string url)
        {
            _mediaPlayerService.Load(url);
        }
    }
}