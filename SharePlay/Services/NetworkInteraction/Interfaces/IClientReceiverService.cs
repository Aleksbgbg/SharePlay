namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using System;

    internal interface IClientReceiverService : IReceiverService
    {
        void Sync(TimeSpan value);
    }
}