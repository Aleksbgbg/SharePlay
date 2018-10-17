namespace SharePlay.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.TeamFoundation.Common;

    internal static class FirewallUtilities
    {
        private static readonly Dictionary<Type, string> FirewallEntityProgIds = new Dictionary<Type, string>
        {
            [typeof(INetFwMgr)] = "HNetCfg.FwMgr",
            [typeof(INetFwOpenPort)] = "HNetCfg.FwOpenPort"
        };

        private static readonly INetFwProfile FirewallProfile;

        static FirewallUtilities()
        {
            FirewallProfile = GetInstance<INetFwMgr>().LocalPolicy.CurrentProfile;
        }

        internal static bool IsOpenPort(int port)
        {
            INetFwOpenPorts openPorts = GetInstance<INetFwMgr>().LocalPolicy.CurrentProfile.GloballyOpenPorts;
            return openPorts.Cast<INetFwOpenPort>().Any(openPort => openPort.Port == port);
        }

        internal static void OpenPort(int port, string application)
        {
            if (IsOpenPort(port)) return;

            INetFwOpenPort openPort = GetInstance<INetFwOpenPort>();
            openPort.Port = port;
            openPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            openPort.Name = application;

            INetFwOpenPorts openPorts = FirewallProfile.GloballyOpenPorts;
            openPorts.Add(openPort);
        }

        internal static void ClosePort(int port)
        {
            if (!IsOpenPort(port)) return;

            INetFwOpenPorts openPorts = FirewallProfile.GloballyOpenPorts;
            openPorts.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
        }

        private static T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(Type.GetTypeFromProgID(FirewallEntityProgIds[typeof(T)]));
        }
    }
}