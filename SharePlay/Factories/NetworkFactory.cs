namespace SharePlay.Factories
{
    using Caliburn.Micro;

    using SharePlay.Factories.Interfaces;
    using SharePlay.Services.Interfaces;

    internal class NetworkFactory : INetworkFactory
    {
        public IPlayClientService MakeClient()
        {
            return IoC.Get<IPlayClientService>();
        }

        public IPlayServerService MakeServer()
        {
            return IoC.Get<IPlayServerService>();
        }
    }
}