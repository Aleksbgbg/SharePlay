namespace SharePlay.Services.NetworkInteraction
{
    using System;

    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class ServerReceiverService : IServerReceiverService
    {
        private readonly IServerSenderService _serverSenderService;

        public ServerReceiverService(IServerSenderService serverSenderService)
        {
            _serverSenderService = serverSenderService;
        }

        public void Progress(TimeSpan value)
        {
            _serverSenderService.Progress = value;
        }

        public void Speed(double value)
        {
            _serverSenderService.Speed = value;
        }

        public void Stop()
        {
            _serverSenderService.Stop();
        }

        public void Play()
        {
            _serverSenderService.Play();
        }

        public void Pause()
        {
            _serverSenderService.Pause();
        }

        public void Load(string url)
        {
            _serverSenderService.Load(url);
        }
    }
}