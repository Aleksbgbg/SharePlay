namespace SharePlay.Utilities
{
    using System;

    using SharePlay.Services.Interfaces;

    internal class ActionBroadcastingUtility
    {
        internal void BroadcastActions(IMediaPlayerService mediaPlayerService, Action<string> broadcastMethod)
        {
            mediaPlayerService.Played += (sender, e) => broadcastMethod("Play");
            mediaPlayerService.Paused += (sender, e) => broadcastMethod("Pause");
        }
    }
}