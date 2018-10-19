namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using System;

    using SharePlay.Models;

    internal interface IClientReceiverService
    {
        MessageContext MessageContext { set; }

        void Progress(TimeSpan value);

        void Volume(double value);

        void Speed(double value);

        void Stop();

        void Play();

        void Pause();

        void Load(string url);
    }
}