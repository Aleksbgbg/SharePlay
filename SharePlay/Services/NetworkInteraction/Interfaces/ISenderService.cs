namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using System;

    internal interface ISenderService
    {
        void Initialize(Action<string> broadcastMethod);
    }
}