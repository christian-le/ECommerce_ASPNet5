using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.EFIdentities
{
    public class DbRoleStore : RoleStore<Role, UserRole, RoleClaim, DbContext, long>
    {
        public DbRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}
