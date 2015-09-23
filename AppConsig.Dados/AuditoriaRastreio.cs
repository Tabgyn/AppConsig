using System.Data.Entity;
using System.Linq;
using AppConsig.Comum.Interfaces;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public static class AuditoriaRastreio
    {
        public static void Auditar(AppContexto dbContext, object nomeUsuario)
        {
            foreach (
                var entrada in dbContext.ChangeTracker.Entries()
                .Where(x => x.Entity is IEntidadeAuditavel &&
                            (x.State == EntityState.Added ||
                             x.State == EntityState.Modified ||
                             x.State == EntityState.Deleted)))
            {
                Auditor auditor;
                Auditoria auditoria;
                switch (entrada.State)
                {
                    case EntityState.Added:
                        auditor = new Auditor(entrada);
                        auditoria = auditor.CriarAuditoria(nomeUsuario, TipoEvento.Incluido, dbContext);

                        if (auditoria != null)
                        {
                            dbContext.Auditorias.Add(auditoria);
                        }
                        break;
                    case EntityState.Modified:
                        auditor = new Auditor(entrada);
                        auditoria = auditor.CriarAuditoria(nomeUsuario, TipoEvento.Atualizado, dbContext);

                        if (auditoria != null)
                        {
                            dbContext.Auditorias.Add(auditoria);
                        }
                        break;
                    case EntityState.Deleted:
                        auditor = new Auditor(entrada);
                        auditoria = auditor.CriarAuditoria(nomeUsuario, TipoEvento.Excluido, dbContext);

                        if (auditoria != null)
                        {
                            dbContext.Auditorias.Add(auditoria);
                        }
                        break;
                }
            }
        }
    }
}