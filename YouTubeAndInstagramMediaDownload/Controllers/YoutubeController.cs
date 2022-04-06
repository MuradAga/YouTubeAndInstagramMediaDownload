using Microsoft.AspNetCore.Mvc;
using VideoLibrary;
using YouTubeAndInstagramMediaDownload.BLL.Services;
using YouTubeAndInstagramMediaDownload.Models;

namespace YouTubeAndInstagramMediaDownload.Controllers
{
    public class YoutubeController : Controller
    {
        private readonly FileService _fileService;
        public YoutubeController(FileService fileService)
        {
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Download(VideoModel videoModel)
        {
            videoModel.Url = videoModel.Url.Trim();

            try
            {
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(videoModel.Url);
                return _fileService.DownloadFile(video.GetBytes(), video.FullName);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
