namespace SharePlay.Utilities
{
    using System;
    using System.Diagnostics;

    internal static class TimeUtility
    {
        private static readonly Stopwatch Stopwatch = new Stopwatch();

        private static TimeSpan syncOffset;

        internal static TimeSpan TimeSinceSyncEpoch => Stopwatch.Elapsed + syncOffset;

        internal static void Start()
        {
            Stopwatch.Start();
        }

        internal static void SyncWithOffset(TimeSpan epoch)
        {
            Stopwatch.Reset();
            Stopwatch.Start();
            syncOffset = epoch;
        }
    }
}