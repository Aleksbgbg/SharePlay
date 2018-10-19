namespace SharePlay.Models
{
    using System;

    internal class MessageContext
    {
        internal MessageContext(DateTime messageSentTime)
        {
            MessageDelay = DateTime.UtcNow - messageSentTime;
        }

        public TimeSpan MessageDelay { get; }
    }
}