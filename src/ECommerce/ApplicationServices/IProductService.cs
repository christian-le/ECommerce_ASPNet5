using ECommerce.Models;

namespace ECommerce.ApplicationServices
{
    public interface IProductService
    {
        void Create(Product product);

        void Update(Product product);

        void Delete(Product product);
    }
}
