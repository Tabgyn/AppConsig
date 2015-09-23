using System.Data.Entity.ModelConfiguration;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public class AuditoriaConfig : EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaConfig()
        {
            HasMany(a => a.DetalhesAuditoria);
        }
    }
}