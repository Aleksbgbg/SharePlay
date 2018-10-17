namespace SharePlay.Services
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Mono.Nat;

    using SharePlay.Services.Interfaces;

    internal class NetworkService : INetworkService
    {
        private const int NetworkPort = 3555;

        private INatDevice _mainNatDevice;

        ~NetworkService()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public IPAddress ExternalIp { get; private set; }

        public int Port => NetworkPort;

        public Task ConfigureMachineForHosting()
        {
            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            void NatUtilityDeviceFound(object sender, DeviceEventArgs e)
            {
                NatUtility.StopDiscovery();
                NatUtility.DeviceFound -= NatUtilityDeviceFound;

                Setup(e.Device);

                taskCompletionSource.SetResult(null);
            }

            NatUtility.DeviceFound += NatUtilityDeviceFound;
            NatUtility.StartDiscovery();

            return taskCompletionSource.Task;
        }

        private static Mapping PortMap => new Mapping(Protocol.Tcp, NetworkPort, NetworkPort);

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            _mainNatDevice.DeletePortMap(PortMap);
        }

        private void Setup(INatDevice mainNatDevice)
        {
            _mainNatDevice = mainNatDevice;

            ExternalIp = mainNatDevice.GetExternalIP();

            mainNatDevice.CreatePortMap(PortMap);
        }
    }
}