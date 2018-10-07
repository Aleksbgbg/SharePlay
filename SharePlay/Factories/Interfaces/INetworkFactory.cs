namespace SharePlay.Factories.Interfaces
{
    using SharePlay.Services.Interfaces;

    internal interface INetworkFactory
    {
        IPlayClientService MakeClient();

        IPlayServerService MakeServer();
    }
}