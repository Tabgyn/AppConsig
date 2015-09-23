using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
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
        public IDbSet<DetalheAuditoria> DetalhesAuditoria{ get; set; }
        public IDbSet<Aviso> Avisos { get; set; }
        public IDbSet<Orgao> Orgaos { get; set; }
        public IDbSet<Perfil> Perfis { get; set; }
        public IDbSet<Permissao> Permissoes { get; set; }
        public IDbSet<SistemaFolha> SistemasFolha { get; set; }
        public IDbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new PerfilConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());

            base.OnModelCreating(modelBuilder);
        }

        public virtual int SaveChanges(object nomeUsuario)
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

                var agora = DateTime.Now;

                switch (entrada.State)
                {
                    case EntityState.Added:
                        entidade.CriadoPor = nomeUsuario.ToString();
                        entidade.DataCriacao = agora;
                        entidade.Excluido = false;
                        break;
                    case EntityState.Modified:
                        Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                        Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                        break;
                    case EntityState.Deleted:
                        entidade.Excluido = true;
                        entrada.State = EntityState.Modified;
                        Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                        Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                        break;
                }
                
                entidade.AtualizadoPor = nomeUsuario.ToString();
                entidade.DataAtualizacao = agora;
            }

            var result = base.SaveChanges();
            
            AuditoriaRastreio.Auditar(this, nomeUsuario);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChanges(null);
        }
    }
}