namespace SharePlay.Services.Interfaces
{
    using System;
    using System.Net;

    internal interface INetworkService : IDisposable
    {
        IPAddress ClientExternalIp { get; }
    }
}