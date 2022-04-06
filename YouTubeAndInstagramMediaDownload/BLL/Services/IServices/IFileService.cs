using Microsoft.AspNetCore.Mvc;

namespace YouTubeAndInstagramMediaDownload.BLL.Services.IServices
{
    public interface IFileService
    {
        public FileResult DownloadFile(byte[] fileBytes, string fileName);
    }
}
