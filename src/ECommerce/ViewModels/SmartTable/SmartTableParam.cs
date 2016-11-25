using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ViewModels.SmartTable
{
    public class SmartTableParam
    {
        public Pagination Pagination { get; set; }

        public Search Search { get; set; }

        public Sort Sort { get; set; }
    }
}
