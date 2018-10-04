namespace StreamServer.Controllers
{
    using System.Diagnostics;
    using System.Net;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[Controller]")]
    public class StreamController : ControllerBase
    {
        [HttpGet("{videoUrl}")]
        public IActionResult GetVideoStream(string videoUrl)
        {
            string decodedUrl = WebUtility.UrlDecode(videoUrl);

            Process streamProcess = Process.Start(new ProcessStartInfo(@"Resources\youtube-dl.exe", $"{decodedUrl} -o -")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

            return new FileStreamResult(streamProcess.StandardOutput.BaseStream, "application/octet-stream");
        }
    }
}