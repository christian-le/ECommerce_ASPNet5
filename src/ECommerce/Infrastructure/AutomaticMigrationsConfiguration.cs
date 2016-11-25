using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.ServiceLocation;
using ECommerce.Models;
using ECommerce.EFIdentities;

namespace ECommerce.Infrastructure
{
    public class AutomaticMigrationsConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public AutomaticMigrationsConfiguration()
        {
            // During the initial development you can turn on this to speed up coding
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override async void Seed(ApplicationDbContext context)
        {
            var roleManager = ServiceLocator.Current.GetInstance<RoleManager<Role>>();

            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                adminRole = new Role { Name = "admin" };
                await roleManager.CreateAsync(adminRole);

                var userManager = ServiceLocator.Current.GetInstance<UserManager<User>>();
                var adminUser = new User
                {
                    UserName = "admin@ecommerce.com",
                    Email = "admin@ecommerce.com",
                    FullName = "System Admin"
                };
                await userManager.CreateAsync(adminUser, "1qazZAQ!");
                await userManager.AddToRoleAsync(adminUser, adminRole.Name);


                var sqlRepository = ServiceLocator.Current.GetInstance<ISqlRepository>();
                sqlRepository.CreateInitData();
            }

            base.Seed(context);
        }

    }
}
