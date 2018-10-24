namespace SharePlay.Services
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.Services.NetworkInteraction.Interfaces;
    using SharePlay.Utilities;

    using SimpleTCP;

    internal class PlayClientService : IPlayClientService
    {
        private readonly IClientReceiverService _clientReceiverService;

        private readonly SimpleTcpClient _tcpClient = new SimpleTcpClient();

        public PlayClientService(IClientReceiverService clientReceiverService, IClientSenderService clientSenderService)
        {
            _clientReceiverService = clientReceiverService;

            _tcpClient.Delimiter = (byte)'\n';
            clientSenderService.Initialize(_tcpClient.WriteLine);
        }

        public async Task<bool> TryConnect(NetworkAddress networkAddress)
        {
            bool didConnect = await Task.Run(() =>
            {
                try
                {
                    _tcpClient.Connect(networkAddress.IpAddress.ToString(), networkAddress.Port);
                    return true;
                }
                catch (SocketException)
                {
                    return false;
                }
            });

            if (didConnect)
            {
                // Measure latency
                Stopwatch stopwatch = Stopwatch.StartNew();

                Message reply = _tcpClient.WriteLineAndGetReply("#", TimeSpan.FromSeconds(5));

                stopwatch.Stop();

                if (reply.MessageString.StartsWith("#")) // Direct communication directive
                {
                    string replyNewlineTrimmed = reply.MessageString.TrimEnd('\r', '\n');

                    // Invisible UTF characters present at the front of the message (skip all non-digit characters
                    // to get offset ticks)
                    string replyFrontTrimmed = new string(replyNewlineTrimmed.SkipWhile(character => !char.IsDigit(character))
                                                                             .ToArray());

                    long offsetTicks = long.Parse(replyFrontTrimmed);

                    // Halfway travel time calculation to sync TimeUtility accurately
                    TimeUtility.SyncWithOffset(TimeSpan.FromTicks(offsetTicks) + TimeSpan.FromTicks(stopwatch.ElapsedTicks / 2));

                    stopwatch.Stop();
                }
                else
                {
                    return false;
                }

                _tcpClient.DelimiterDataReceived += (sender, e) => _clientReceiverService.Receive(e.MessageString);
            }

            return didConnect;
        }
    }
}