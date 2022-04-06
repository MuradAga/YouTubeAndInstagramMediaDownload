using Microsoft.AspNetCore.Mvc;
using YouTubeAndInstagramMediaDownload.Models;
using System.Net;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing;
using YouTubeAndInstagramMediaDownload.BLL.Services;

namespace YouTubeAndInstagramMediaDownload.Controllers
{
    public class InstagramController : Controller
    {
        private readonly FileService _fileService;
        public InstagramController(FileService fileService)
        {
            _fileService = fileService;
        }
        public IActionResult Photo()
        {
            return View("Photo");
        }
        public IActionResult Video()
        {
            return View("Video");
        }
        public IActionResult DownloadPhoto(VideoModel videoModel)
        {
            videoModel.Url = videoModel.Url.Trim();

            try
            {
                videoModel.Url = videoModel.Url;

                if (videoModel.Url.Contains("?utm_source=ig_web_copy_link"))
                {
                    var parts = videoModel.Url.Split("/");
                    videoModel.Url = parts[0] + @"//" + parts[2] + '/' + parts[3] + '/' + parts[4] + '/';
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                WebClient webClient = new WebClient();
                dynamic val = JsonConvert.DeserializeObject<object>(webClient.DownloadString(videoModel.Url + "?__a=1"));
                string address = val["graphql"]["shortcode_media"]["display_url"];
                string str = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                Stream stream = webClient.OpenRead(address);
                new Bitmap(stream)?.Save(Path.Combine(path, "IG_Image_" + str + ".jpg"), ImageFormat.Jpeg);
                stream.Flush();
                stream.Close();
                webClient.Dispose();

                return View("Photo");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public IActionResult DownloadVideo(VideoModel videoModel)
        {
            videoModel.Url = videoModel.Url.Trim();

            try
            {
                videoModel.Url = videoModel.Url;

                if (videoModel.Url.Contains("?utm_source=ig_web_copy_link"))
                {
                    var parts = videoModel.Url.Split("/");
                    videoModel.Url = parts[0] + @"//" + parts[2] + '/' + parts[3] + '/' + parts[4] + '/';
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                WebClient webClient = new WebClient();
                string value = webClient.DownloadString(videoModel.Url + "?__a=1");
                string str = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                dynamic val = JsonConvert.DeserializeObject<object>(value);
                string address = val["graphql"]["shortcode_media"]["video_url"];
                webClient.DownloadFile(address, Path.Combine(path, "IG_Video_" + str + ".mp4"));

                return View("Video");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
