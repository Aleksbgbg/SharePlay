namespace SharePlay.Models
{
    using System;
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

        public static NetworkAddress Parse(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentOutOfRangeException(nameof(address), address, "Address does not contain any characters.");
            }

            if (address.IndexOf(':') == -1)
            {
                throw new ArgumentException("Address must be in the format '{IP}:{Port}' - colon was not found.", nameof(address));
            }

            string[] components = address.Split(new[] { ':' }, 2);

            IPAddress ipAddress = IPAddress.Parse(components[0]);
            int port = int.Parse(components[1]);

            return new NetworkAddress(ipAddress, port);
        }

        public override string ToString()
        {
            return string.Concat(IpAddress, ":", Port);
        }
    }
}