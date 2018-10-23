namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using System;

    using SharePlay.Models;

    internal interface IClientReceiverService : IReceiverService
    {
        MessageContext MessageContext { set; }

        void Sync(TimeSpan value);
    }
}