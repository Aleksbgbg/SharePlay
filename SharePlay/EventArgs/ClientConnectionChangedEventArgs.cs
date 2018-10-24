namespace SharePlay.EventArgs
{
    using System;
    using System.Net;

    internal class ClientConnectionChangedEventArgs : EventArgs
    {
        public ClientConnectionChangedEventArgs(IPAddress address)
        {
            Address = address;
        }

        public IPAddress Address { get; }
    }
}