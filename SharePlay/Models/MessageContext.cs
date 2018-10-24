namespace SharePlay.Models
{
    using System;

    internal class MessageContext
    {
        internal MessageContext(TimeSpan messageDelay)
        {
            MessageDelay = messageDelay;
        }

        public TimeSpan MessageDelay { get; }
    }
}