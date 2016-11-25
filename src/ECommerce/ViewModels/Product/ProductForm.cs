using Microsoft.AspNet.Http;
using System.Collections.Generic;

namespace ECommerce.ViewModels.Product
{
    public class ProductForm
    {
        public ProductViewModel Product { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public IList<IFormFile> ProductImages { get; set; } = new List<IFormFile>();

        public IList<IFormFile> File { get; set; } = new List<IFormFile>();
    }
}
