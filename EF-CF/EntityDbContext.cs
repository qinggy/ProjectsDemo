using EF_CF_Data.Mappings;
using EF_CF_Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EF_CF_Data
{
    public class EntityDbContext : DbContext
    {
        private static readonly string _connectionString = "";
        private static readonly IEnumerable<IEntityMapping> _mappings;
        private static readonly CompositionContainer _compositionContainer;

        static EntityDbContext()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IEntityMapping).Assembly));

            _compositionContainer = new CompositionContainer(catalog);
            _mappings = _compositionContainer.GetExportedValues<IEntityMapping>();
            _connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString.ToString();
        }

        public EntityDbContext()
            : base(_connectionString)
        {
            Database.SetInitializer<EntityDbContext>(new EntityDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (_mappings != null)
            {
                foreach (IEntityMapping mapping in _mappings)
                {
                    mapping.Configure(modelBuilder);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


    }

    public class EntityDbInitializer : DropCreateDatabaseAlways<EntityDbContext>
    {
        protected override void Seed(EntityDbContext context)
        {
            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Id = Guid.NewGuid(), Name = "卿光扬", CardId = "458768598725014562", Role = new Role() { Id = Guid.NewGuid(), Name = "管理员" } });

            foreach (User std in defaultUsers)
                context.Set<User>().Add(std);

            base.Seed(context);
        }
    }
}
