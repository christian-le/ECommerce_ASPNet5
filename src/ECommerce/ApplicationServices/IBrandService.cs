using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ApplicationServices
{
    public interface IBrandService
    {
        void Create(Brand brand);

        void Update(Brand brand);

        void Delete(long id);

        void Delete(Brand brand);
    }
}
