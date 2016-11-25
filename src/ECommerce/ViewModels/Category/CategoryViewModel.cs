using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ViewModels.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            IsPublished = true;
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public long? ParentId { get; set; }

        public bool IsPublished { get; set; }
    }
}
