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

        public IDbSet<Auditoria> Auditorias { get; set; }
        public IDbSet<Aviso> Avisos { get; set; }
        public IDbSet<Departamento> Departamentos { get; set; }
        public IDbSet<Perfil> Perfis { get; set; }
        public IDbSet<Permissao> Permissoes { get; set; }
        public IDbSet<SistemaFolha> SistemasFolha { get; set; }
        public IDbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Add entities config
            modelBuilder.Configurations.Add(new AuditoriaConfig());
            modelBuilder.Configurations.Add(new DepartamentoConfig());
            modelBuilder.Configurations.Add(new SistemaFolhaConfig());
            modelBuilder.Configurations.Add(new AvisoConfig());
            modelBuilder.Configurations.Add(new PermissaoConfig());
            modelBuilder.Configurations.Add(new PerfilConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());

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