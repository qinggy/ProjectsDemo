using IS.Esd.Domain.Entities;
using IS.Esd.Domain.Mapping;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain
{
    public class VersionDbContext : DbContext
    {
        private static readonly string _ConnectionString;

        static VersionDbContext()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString.ToString();
        }

        public VersionDbContext()
            : base(_ConnectionString)
        {
            Database.SetInitializer<VersionDbContext>(new DbContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add<Account>(new AccountMapping())
                .Add<Client>(new ClientMapping())
                .Add<ClientVersion>(new ClientVersionMapping())
                .Add<Module>(new ModuleMapping())
                .Add<Product>(new ProductMapping())
                .Add<Role>(new RoleMapping())
                .Add<IS.Esd.Domain.Entities.Version>(new VersionMapping());

            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<ClientVersion> ClientVersion { get; set; }
        //public DbSet<Module> Modules { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<IS.Esd.Domain.Entities.Version> Versions { get; set; }
    }

    class DbContextInitializer : CreateDatabaseIfNotExists<VersionDbContext>
    {
        protected override void Seed(VersionDbContext context)
        {
            base.Seed(context);


        }
    }
}
