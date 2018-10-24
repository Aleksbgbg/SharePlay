namespace SharePlay.Services.Interfaces
{
    using System;

    using SharePlay.EventArgs;

    internal interface IPlayServerService
    {
        event EventHandler<ClientConnectionChangedEventArgs> ClientConnected;

        event EventHandler<ClientConnectionChangedEventArgs> ClientDisconnected;

        void Host(int port);
    }
}