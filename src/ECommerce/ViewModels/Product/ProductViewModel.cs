using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ViewModels.Product
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            IsPublished = true;
        }

        public long Id { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public bool IsPublished { get; set; }

        public IList<long> CategoryIds { get; set; } = new List<long>();

        public string ThumbnailImageUrl { get; set; }

        public IList<ProductMediaVm> ProductMedias { get; set; } = new List<ProductMediaVm>();

        public IList<long> DeletedMediaIds { get; set; } = new List<long>();

        public long? BrandId { get; set; }
    }
}
