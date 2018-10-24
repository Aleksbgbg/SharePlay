namespace SharePlay.Services.NetworkInteraction
{
    using System;

    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class ServerReceiverService : ReceiverService, IServerReceiverService
    {
        private readonly IServerSenderService _serverSenderService;

        public ServerReceiverService(IServerSenderService serverSenderService)
        {
            _serverSenderService = serverSenderService;
        }

        private void Progress(TimeSpan value)
        {
            _serverSenderService.Progress = value;
        }

        private void Speed(double value)
        {
            _serverSenderService.Speed = value;
        }

        private void Stop()
        {
            _serverSenderService.Stop();
        }

        private void Play()
        {
            _serverSenderService.Play();
        }

        private void Pause()
        {
            _serverSenderService.Pause();
        }

        private void Load(string url)
        {
            _serverSenderService.Load(url);
        }
    }
}