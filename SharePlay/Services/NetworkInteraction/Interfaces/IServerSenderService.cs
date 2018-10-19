namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using System;

    using SharePlay.Services.Interfaces;

    internal interface IServerSenderService : IMediaPlayerService
    {
        void Initialize(Action<string> broadcastMethod);
    }
}