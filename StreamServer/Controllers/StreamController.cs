namespace StreamServer.Controllers
{
    using System.IO;
    using System.Net;

    using Microsoft.AspNetCore.Mvc;

    using StreamServer.Services.Interfaces;

    [ApiController]
    [Route("[Controller]")]
    public class StreamController : ControllerBase
    {
        private readonly IVideoStreamingService _videoStreamingService;

        public StreamController(IVideoStreamingService videoStreamingService)
        {
            _videoStreamingService = videoStreamingService;
        }

        [HttpGet("{videoUrl}")]
        public IActionResult GetVideoStream(string videoUrl)
        {
            string decodedUrl = WebUtility.UrlDecode(videoUrl);

            Stream videoStream = _videoStreamingService.StreamVideo(decodedUrl);

            return new FileStreamResult(videoStream, "application/octet-stream");
        }
    }
}