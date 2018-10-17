namespace SharePlay.Services.Interfaces
{
    using System.Threading.Tasks;

    using SharePlay.Models;

    internal interface IPlayClientService
    {
        Task<bool> TryConnect(NetworkAddress networkAddress);
    }
}