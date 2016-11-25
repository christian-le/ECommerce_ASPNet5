using System.Data.Entity;

namespace ECommerce.Infrastructure
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
        where T : class, IEntityWithTypedId<long>
    {
        public Repository(DbContext context) : base(context)
        {
        }
    }
}
