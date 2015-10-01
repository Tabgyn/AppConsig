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

        public override int SaveChanges()
        {
            var nomeUsuario = Thread.CurrentPrincipal.Identity.Name;
            
            Auditar(this, nomeUsuario);
            
            return base.SaveChanges();
        }
        
        private void Auditar(DbContext contexto, string nomeUsuario)
        {
            var dataAtual = DateTime.Now;
            var entradas = contexto.ChangeTracker.Entries()
                .Where(x => x.Entity is IEntidadeAuditavel &&
                            (x.State == EntityState.Added ||
                             x.State == EntityState.Modified ||
                             x.State == EntityState.Deleted));

            foreach (var entrada in entradas)
            {
                var entidade = entrada.Entity as IEntidadeAuditavel;

                if (entidade == null) continue;

                entidade.AtualizadoPor = nomeUsuario;
                entidade.DataAtualizacao = dataAtual;

                var auditoria = new Auditoria
                {
                    DataEvento = dataAtual,
                    Usuario = nomeUsuario,
                    NomeEntidade = entidade.GetType().Name,
                    RegistroId = entrada.CurrentValues["Id"].ToString()
                };

                switch (entrada.State)
                {
                    case EntityState.Added:
                        entidade.CriadoPor = nomeUsuario;
                        entidade.DataCriacao = dataAtual;
                        entidade.Excluido = false;
                        auditoria.TipoEvento = TipoEvento.Incluido.ToString();
                        foreach (var prop in entrada.CurrentValues.PropertyNames)
                        {
                            auditoria.DetalhesAuditoria.Add(new DetalheAuditoria
                            {
                                Propriedade = prop,
                                ValorOriginal = string.Empty,
                                ValorNovo = Convert.ToString(entrada.CurrentValues[prop])
                                
                            });
                        }
                        break;
                    case EntityState.Modified:
                        auditoria.TipoEvento = TipoEvento.Atualizado.ToString();
                        foreach (var prop in entrada.GetDatabaseValues().PropertyNames)
                        {
                            var currentValue = Convert.ToString(entrada.CurrentValues[prop]);
                            var originalValue = Convert.ToString(entrada.GetDatabaseValues()[prop]);
                            if (!currentValue.Equals(originalValue))
                            {
                                auditoria.DetalhesAuditoria.Add(new DetalheAuditoria
                                {
                                    Propriedade = prop,
                                    ValorOriginal = originalValue,
                                    ValorNovo = currentValue
                                });
                            }
                        }
                        Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                        Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                        break;
                    case EntityState.Deleted:
                        auditoria.TipoEvento = TipoEvento.Excluido.ToString();
                        entidade.Excluido = true;
                        entrada.State = EntityState.Modified;
                        Entry(entidade).Property(x => x.CriadoPor).IsModified = false;
                        Entry(entidade).Property(x => x.DataCriacao).IsModified = false;
                        break;
                }
                
                Auditorias.Add(auditoria);
            }
        }
    }
}