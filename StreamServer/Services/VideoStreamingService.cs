namespace StreamServer.Services
{
    using System.Diagnostics;
    using System.IO;

    using StreamServer.Services.Interfaces;

    public class VideoStreamingService : IVideoStreamingService
    {
        public Stream StreamVideo(string url)
        {
            using (Process streamProcess = Process.Start(new ProcessStartInfo("Resources/youtube-dl.exe", $"-o - -- {url}")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            }))
            {
                return streamProcess.StandardOutput.BaseStream;
            }
        }
    }
}