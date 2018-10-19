namespace SharePlay.Factories.Interfaces
{
    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal interface INetworkInteractionFactory
    {
        IClientReceiverService MakeClientReceiverService();

        IClientSenderService MakeClientSenderService();

        IServerReceiverService MakeServerReceiverService();

        IServerSenderService MakeServerSenderService();
    }
}