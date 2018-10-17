namespace SharePlay.Services.Interfaces
{
    using SharePlay.Models;

    internal interface IPlayClientService
    {
        void Connect(NetworkAddress networkAddress);
    }
}