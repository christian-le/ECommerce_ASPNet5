using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.EFIdentities
{
    public class DbUserStore : UserStore<User, Role, UserRole, UserClaim,
        UserLogin, RoleClaim, DbContext, long>
    {
        public DbUserStore(DbContext context)
            : base(context)
        {
        }
    }
}
