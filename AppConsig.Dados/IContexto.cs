using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public interface IContexto
    {
        IDbSet<Aviso> Avisos { get; set; }
        IDbSet<Perfil> Perfil { get; set; }
        IDbSet<Permissao> Permissoes { get; set; }
        IDbSet<Usuario> Usuarios { get; set; }
        
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges(); 
    }
}