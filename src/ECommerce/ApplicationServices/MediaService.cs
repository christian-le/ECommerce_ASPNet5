using ECommerce.Infrastructure;
using ECommerce.Models;
using System.IO;

namespace ECommerce.ApplicationServices
{
    public class MediaService : IMediaService
    {
        private const string MediaRootFoler = "UserContents";

        private IRepository<Media> mediaRespository;

        public MediaService(IRepository<Media> mediaRespository)
        {
            this.mediaRespository = mediaRespository;
        }

        public string GetMediaUrl(Media media)
        {
            if (media != null)
            {
                return $"/{MediaRootFoler}/{media.FileName}";
            }

            return $"/{MediaRootFoler}/default.png";
        }

        public string GetMediaUrl(string fileName)
        {
            return $"/{MediaRootFoler}/{fileName}";
        }

        public string GetThumbnailUrl(Media media)
        {
            return GetMediaUrl(media);
        }

        public void SaveMedia(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(GlobalConfiguration.ApplicationPath, MediaRootFoler, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                mediaBinaryStream.CopyTo(output);
            }
        }

        public void DeleteMedia(Media media)
        {
            mediaRespository.Remove(media);
            var filePath = Path.Combine(GlobalConfiguration.ApplicationPath, MediaRootFoler, media.FileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
