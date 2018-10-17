namespace SharePlay.Services.Interfaces
{
    using System;

    using SharePlay.EventArgs;

    internal interface IPlayServerService
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;

        event EventHandler<ClientConnectedEventArgs> ClientDisconnected;

        void Host(int port);
    }
}