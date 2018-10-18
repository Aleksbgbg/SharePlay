namespace SharePlay.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Caliburn.Micro;

    using SharePlay.Services.Interfaces;

    internal class ActionBroadcastingUtility
    {
        private readonly IMediaPlayerService _mediaPlayerService;

        private readonly MethodInfo[] _playerMethods;

        internal ActionBroadcastingUtility(IMediaPlayerService mediaPlayerService)
        {
            _mediaPlayerService = mediaPlayerService;

            _playerMethods = mediaPlayerService.GetType().GetMethods();
        }

        internal void BroadcastAllActions(Action<string> broadcastMethod)
        {
            _mediaPlayerService.Played += (sender, e) => broadcastMethod("Play");
            _mediaPlayerService.Paused += (sender, e) => broadcastMethod("Pause");
        }

        internal void ReceiveAction(string action)
        {
            string[] commands = action.Split(' ');

            string methodName = commands[0];

            object[] arguments = commands.Length == 1 ? null : commands.Skip(1).Cast<object>().ToArray();

            MethodInfo targetMethod = _playerMethods.Single(method => method.Name == methodName);

            Execute.BeginOnUIThread(() => targetMethod.Invoke(_mediaPlayerService, arguments));
        }
    }
}