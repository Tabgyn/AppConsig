using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AppConsig.Entities;

namespace AppConsig.Data
{
    public interface IContext
    {
        IDbSet<Auditoria> Auditorias { get; set; }
        IDbSet<Aviso> Avisos { get; set; }
        IDbSet<Departamento> Departamentos { get; set; }
        IDbSet<Perfil> Perfis { get; set; }
        IDbSet<Permissao> Permissoes { get; set; }
        IDbSet<SistemaFolha> SistemasFolha { get; set; }
        IDbSet<Usuario> Usuarios { get; set; }
        
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges(); 
    }
}