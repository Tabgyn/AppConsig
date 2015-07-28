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

        public IDbSet<Aviso> Avisos { get; set; }
        public IDbSet<Perfil> Perfil { get; set; }
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
                .Where(x => x.Entity is IEntidadeAuditavel
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entrada in entradasModificadas)
            {
                var entidade = entrada.Entity as IEntidadeAuditavel;

                if (entidade == null) continue;

                var nomeUsuario = Thread.CurrentPrincipal.Identity.Name;

                var dataAgora = DateTime.Now;

                if (entrada.State == EntityState.Added)
                {
                    entidade.CriadoPor = nomeUsuario;
                    entidade.DataCriacao = dataAgora;
                    entidade.Excluido = false;
                }
                else
                {
                    Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                    Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                }

                entidade.AtualizadoPor = nomeUsuario;
                entidade.DataAtualizacao = dataAgora;
            }

            return base.SaveChanges();
        }

        public System.Data.Entity.DbSet<AppConsig.Entidades.Perfil> Perfils { get; set; }
    }
}