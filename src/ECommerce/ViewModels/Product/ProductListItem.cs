﻿using System;

namespace ECommerce.ViewModels.Product
{
    public class ProductListItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublished { get; set; }
    }
}
