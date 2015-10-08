using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class AuditConfig : EntityTypeConfiguration<Audit>
    {
        public AuditConfig()
        {
            ToTable("Auditoria");

            HasKey(e => e.Id);
            
            Property(e => e.UserName).HasColumnName("Usuario").IsRequired();
            Property(e => e.SessaoId).HasColumnName("Sessao").IsRequired();
            Property(e => e.Action).HasColumnName("Metodo").IsRequired();
            Property(e => e.Controller).HasColumnName("Evento").IsRequired();
            Property(e => e.AuditDate).HasColumnName("DataEvento").IsRequired();
            Property(e => e.IpAddress).HasColumnName("Ip").IsRequired();
            Property(e => e.Data).HasColumnName("Dados").IsRequired();
        }
    }
}