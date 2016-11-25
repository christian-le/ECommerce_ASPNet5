using ECommerce.Infrastructure;
using System;
using System.Collections.Generic;

namespace ECommerce.EFIdentities
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, IEntityWithTypedId<long>
    {
        public User()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public string FullName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}