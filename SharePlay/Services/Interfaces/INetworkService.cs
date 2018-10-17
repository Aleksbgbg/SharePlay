namespace SharePlay.Services.Interfaces
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    internal interface INetworkService : IDisposable
    {
        IPAddress ExternalIp { get; }

        int Port { get; }

        Task ConfigureMachineForHosting();
    }
}