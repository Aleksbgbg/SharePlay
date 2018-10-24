namespace SharePlay.Services.NetworkInteraction
{
    using System;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    internal class ClientReceiverService : ReceiverService, IClientReceiverService
    {
        private readonly IMediaPlayerService _mediaPlayerService;

        private MessageContext _messageContext;

        public ClientReceiverService(IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;
        }

        private protected override int MethodNameIndex => 1;

        public void Sync(TimeSpan value)
        {
            TimeSpan realProgress = value + _messageContext.MessageDelay;

            if (Math.Abs(_mediaPlayerService.Progress.TotalSeconds - realProgress.TotalSeconds) > 0.1)
            {
                _mediaPlayerService.Progress = realProgress;
            }
        }

        private protected override void Process(string[] commands)
        {
            _messageContext = new MessageContext(TimeSpan.FromTicks(long.Parse(commands[0])) - TimeUtility.TimeSinceSyncEpoch);
        }

        private void Progress(TimeSpan value)
        {
            _mediaPlayerService.Progress = value + _messageContext.MessageDelay;
        }

        private void Speed(double value)
        {
            _mediaPlayerService.Progress += TimeSpan.FromSeconds((value - _mediaPlayerService.Speed) * _messageContext.MessageDelay.TotalSeconds);
            _mediaPlayerService.Speed = value;
        }

        private void Stop()
        {
            _mediaPlayerService.Stop();
            _mediaPlayerService.Progress -= _messageContext.MessageDelay;
        }

        private void Play()
        {
            _mediaPlayerService.Play();
            _mediaPlayerService.Progress += _messageContext.MessageDelay;
        }

        private void Pause()
        {
            _mediaPlayerService.Pause();
            _mediaPlayerService.Progress -= _messageContext.MessageDelay;
        }

        private void Load(string url)
        {
            _mediaPlayerService.Load(url);
        }
    }
}