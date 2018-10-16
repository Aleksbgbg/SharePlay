namespace SharePlay.Models
{
    using System.Net;

    internal class NetworkAddress
    {
        public NetworkAddress(IPAddress ipAddress, int port)
        {
            IpAddress = ipAddress;
            Port = port;
        }

        public IPAddress IpAddress { get; }

        public int Port { get; }

        public override string ToString()
        {
            return string.Concat(IpAddress, ":", Port);
        }
    }
}