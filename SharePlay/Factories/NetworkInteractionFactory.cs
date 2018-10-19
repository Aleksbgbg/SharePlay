namespace SharePlay.Factories
{
    using Caliburn.Micro;

    using SharePlay.Factories.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class NetworkInteractionFactory : INetworkInteractionFactory
    {
        public IClientReceiverService MakeClientReceiverService()
        {
            return IoC.Get<IClientReceiverService>();
        }

        public IClientSenderService MakeClientSenderService()
        {
            return IoC.Get<IClientSenderService>();
        }

        public IServerReceiverService MakeServerReceiverService()
        {
            return IoC.Get<IServerReceiverService>();
        }

        public IServerSenderService MakeServerSenderService()
        {
            return IoC.Get<IServerSenderService>();
        }
    }
}