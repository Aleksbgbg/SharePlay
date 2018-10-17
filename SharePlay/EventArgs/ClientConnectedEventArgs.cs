namespace SharePlay.EventArgs
{
    using System;
    using System.Net;

    internal class ClientConnectedEventArgs : EventArgs
    {
        public ClientConnectedEventArgs(IPAddress address)
        {
            Address = address;
        }

        public IPAddress Address { get; }
    }
}