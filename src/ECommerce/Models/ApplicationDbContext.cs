using ECommerce.Models;
using ECommerce.EFIdentities;
using ECommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace ECommerce.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role,
        long, UserLogin, UserRole, UserClaim, RoleClaim>
    {

        public ApplicationDbContext() : base(GlobalConfiguration.ConnectionString)
        {
        }

        public ApplicationDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, AutomaticMigrationsConfiguration>());

            RegisterConventions(modelBuilder);

            List<Type> typeToRegisters = new List<Type>();
            foreach (var module in GlobalConfiguration.Modules)
            {
                typeToRegisters.AddRange(TypeLoader.FromAssemblies(Assembly.Load(module.AssemblyName)));
            }

            RegisterCustomMapping(modelBuilder, typeToRegisters);

            RegisterEntities(modelBuilder, typeToRegisters);

            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<UrlSlug>().ToTable("UrlSlug");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductMedia>().ToTable("ProductMedia");
            modelBuilder.Entity<Media>().ToTable("Media");

            base.OnModelCreating(modelBuilder);
        }
        private void RegisterConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new TableNameConvention());
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
        }

        private void RegisterEntities(DbModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            var entityTypes = typeToRegisters.Where(x => x.IsSubclassOf(typeof(Entity)) && !x.IsAbstract);
            foreach (var type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        private void RegisterCustomMapping(DbModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var typesToRegister = typeToRegisters
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(
                    type =>
                        type.BaseType != null && type.BaseType.IsGenericType &&
                        type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
