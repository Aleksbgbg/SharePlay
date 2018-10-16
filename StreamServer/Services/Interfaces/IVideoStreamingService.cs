namespace StreamServer.Services.Interfaces
{
    using System.IO;

    public interface IVideoStreamingService
    {
        Stream StreamVideo(string url);
    }
}