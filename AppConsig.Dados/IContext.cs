using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AppConsig.Entities;

namespace AppConsig.Data
{
    public interface IContext
    {
        IDbSet<Audit> Audits { get; set; }
        IDbSet<Notice> Notices { get; set; }
        IDbSet<Department> Departments { get; set; }
        IDbSet<Profile> Profiles { get; set; }
        IDbSet<Permission> Permissions { get; set; }
        IDbSet<HumanResourceSystem> HumanResourceSystems { get; set; }
        IDbSet<User> Users { get; set; }
        
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges(); 
    }
}