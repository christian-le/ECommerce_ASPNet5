﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ViewModels.Category
{
    public class CategoryListItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsPublished { get; set; }
    }
}
