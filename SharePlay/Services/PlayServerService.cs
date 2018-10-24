namespace SharePlay.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    using SharePlay.EventArgs;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayServerService : IPlayServerService
    {
        private readonly SimpleTcpServer _tcpServer = new SimpleTcpServer();

        private readonly IServerReceiverService _serverReceiverService;

        public PlayServerService(IServerReceiverService serverReceiverService, IServerSenderService serverSenderService)
        {
            _serverReceiverService = serverReceiverService;

            _tcpServer.Delimiter = (byte)'\n';
            serverSenderService.Initialize(_tcpServer.BroadcastLine);
        }

        public event EventHandler<ClientConnectionChangedEventArgs> ClientConnected;

        public event EventHandler<ClientConnectionChangedEventArgs> ClientDisconnected;

        public void Host(int port)
        {
            TimeUtility.Start();

            _tcpServer.Start(IPAddress.Any, port);

            _tcpServer.ClientConnected += async (sender, e) =>
            {
                NetworkStream networkStream = e.GetStream();

                using (StreamReader streamReader = new StreamReader(networkStream, Encoding.UTF8,
                                                                    detectEncodingFromByteOrderMarks: true,
                                                                    bufferSize: 1024,
                                                                    leaveOpen: true))
                {
                    string line = await streamReader.ReadLineAsync();

                    if (line != "#") // Expect a handshake
                    {
                        e.Close();
                        return;
                    }

                    using (StreamWriter streamWriter = new StreamWriter(networkStream, Encoding.UTF8,
                                                                        bufferSize: 1024,
                                                                        leaveOpen: true))
                    {
                        streamWriter.WriteLine(string.Concat("#", TimeUtility.TimeSinceSyncEpoch.Ticks));
                    }
                }

                ClientConnected?.Invoke(this, new ClientConnectionChangedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));
            };

            _tcpServer.ClientDisconnected += (sender, e) => ClientDisconnected?.Invoke(this, new ClientConnectionChangedEventArgs(((IPEndPoint)e.Client.RemoteEndPoint).Address));

            _tcpServer.DelimiterDataReceived += (sender, e) =>
            {
                if (!e.MessageString.StartsWith("#")) // Direct communication directive
                {
                    _serverReceiverService.Receive(e.MessageString);
                }
            };
        }
    }
}