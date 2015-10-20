using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class AuditoriaConfig : EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaConfig()
        {
            ToTable("Auditoria");

            HasKey(e => e.Id);
            
            Property(e => e.Usuario).HasColumnName("Usuario").IsRequired();
            Property(e => e.SessaoId).HasColumnName("SessaoId").IsRequired();
            Property(e => e.Acao).HasColumnName("Acao").IsRequired();
            Property(e => e.Controle).HasColumnName("Controle").IsRequired();
            Property(e => e.DataEvento).HasColumnName("DataEvento").IsRequired();
            Property(e => e.EnderecoIP).HasColumnName("EnderecoIP").IsRequired();
            Property(e => e.Dados).HasColumnName("Dados").IsOptional();
        }
    }
}