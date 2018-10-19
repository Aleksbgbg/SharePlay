namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using SharePlay.Models;

    internal interface IClientReceiverService : IReceiverService
    {
        MessageContext MessageContext { set; }
    }
}