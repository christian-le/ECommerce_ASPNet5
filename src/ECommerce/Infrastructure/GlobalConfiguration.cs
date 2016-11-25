using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
            Modules = new List<DbModule>();
        }

        public static string ConnectionString { get; set; }

        public static string ApplicationPath { get; set; }

        public static IList<DbModule> Modules { get; set; }
    }
}
