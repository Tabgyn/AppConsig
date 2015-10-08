using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using AppConsig.Common.Interfaces;
using AppConsig.Data.Configuration;
using AppConsig.Entities;

namespace AppConsig.Data
{
    public class AppContext : DbContext, IContext
    {
        public AppContext()
            : base("Name=AppConsigContexto")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<AppContext, Migrations.Configuration>("AppConsigContexto"));
        }

        public IDbSet<Audit> Audits { get; set; }
        public IDbSet<Notice> Notices { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Profile> Profiles { get; set; }
        public IDbSet<Permission> Permissions { get; set; }
        public IDbSet<HumanResourceSystem> HumanResourceSystems { get; set; }
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Add entities config
            modelBuilder.Configurations.Add(new AuditConfig());
            modelBuilder.Configurations.Add(new DepartamentConfig());
            modelBuilder.Configurations.Add(new HumanResourceSystemConfig());
            modelBuilder.Configurations.Add(new NoticeConfig());
            modelBuilder.Configurations.Add(new PermissionConfig());
            modelBuilder.Configurations.Add(new ProfileConfig());
            modelBuilder.Configurations.Add(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            var timestamp = DateTime.Now;
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditEntity &&
                            (x.State == EntityState.Added ||
                             x.State == EntityState.Modified ||
                             x.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as IAuditEntity;

                if (entity == null) continue;

                entity.UpdateBy = userName;
                entity.UpdateDate = timestamp;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreateBy = userName;
                        entity.CreateDate = timestamp;
                        entity.Deleted = false;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreateBy).IsModified = false;
                        Entry(entity).Property(x => x.CreateDate).IsModified = false;
                        break;
                    case EntityState.Deleted:
                        entity.Deleted = true;
                        entry.State = EntityState.Modified;
                        Entry(entity).Property(x => x.CreateBy).IsModified = false;
                        Entry(entity).Property(x => x.CreateDate).IsModified = false;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}