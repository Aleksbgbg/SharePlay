﻿namespace SharePlay.Services.NetworkInteraction.Interfaces
{
    using System;

    internal interface IReceiverService
    {
        void Progress(TimeSpan value);

        void Volume(double value);

        void Speed(double value);

        void Stop();

        void Play();

        void Pause();

        void Load(string url);
    }
}