﻿using ECommerce.Infrastructure;

namespace ECommerce.Models
{
    public class ProductMedia : Entity
    {
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long MediaId { get; set; }

        public virtual Media Media { get; set; }

        public int DisplayOrder { get; set; }
    }
}
