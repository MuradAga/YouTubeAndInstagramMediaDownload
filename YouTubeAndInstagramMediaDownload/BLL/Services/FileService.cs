using Microsoft.AspNetCore.Mvc;
using YouTubeAndInstagramMediaDownload.BLL.Services.IServices;

namespace YouTubeAndInstagramMediaDownload.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly ControllerBase _controller;
        public FileService(ControllerBase controller)
        {
            _controller = controller;
        }
        public FileResult DownloadFile(byte[] fileBytes, string fileName)
        {
            return _controller.File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
