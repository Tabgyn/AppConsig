using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using AppConsig.Comum.Interfaces;
using AppConsig.Dados.Migrations;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public class AppContexto : DbContext, IContexto
    {
        public AppContexto()
            : base("Name=AppConsigContexto")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<AppContexto, Configuration>("AppConsigContexto"));
        }

        public IDbSet<Auditoria> Auditorias { get; set; }
        public IDbSet<Aviso> Avisos { get; set; }
        public IDbSet<Perfil> Perfis { get; set; }
        public IDbSet<Permissao> Permissoes { get; set; }
        public IDbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new PerfilConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entradasModificadas = ChangeTracker.Entries()
                .Where(x => x.Entity is IEntidadeAuditavel &&
                            (x.State == EntityState.Added ||
                             x.State == EntityState.Modified ||
                             x.State == EntityState.Deleted));

            foreach (var entrada in entradasModificadas)
            {
                var entidade = entrada.Entity as IEntidadeAuditavel;

                if (entidade == null) continue;

                var usuario = Usuarios.First(u => u.Email == Thread.CurrentPrincipal.Identity.Name);

                var dataAgora = DateTime.Now;

                switch (entrada.State)
                {
                    case EntityState.Added:
                        entidade.CriadoPor = usuario.Email;
                        entidade.DataCriacao = dataAgora;
                        entidade.Excluido = false;
                        break;
                    case EntityState.Modified:
                        Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                        Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                        break;
                    case EntityState.Deleted:
                        entrada.State = EntityState.Modified;
                        entidade.Excluido = false;
                        Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                        Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                        break;
                }

                entidade.AtualizadoPor = usuario.Email;
                entidade.DataAtualizacao = dataAgora;
            }

            return base.SaveChanges();
        }
    }
}