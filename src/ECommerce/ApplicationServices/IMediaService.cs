using ECommerce.Models;
using System.IO;

namespace ECommerce.ApplicationServices
{
    public interface IMediaService
    {
        string GetMediaUrl(Media media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(Media media);

        void SaveMedia(Stream mediaBinaryStream, string fileName);

        void DeleteMedia(Media media);
    }
}
