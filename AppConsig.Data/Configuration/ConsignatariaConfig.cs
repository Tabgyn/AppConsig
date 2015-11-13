using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class ConsignatariaConfig : EntityTypeConfiguration<Consignataria>
    {
        public ConsignatariaConfig()
        {
            ToTable("Consignataria");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Sigla).HasColumnName("Sigla").IsRequired();
            Property(e => e.CNPJ).HasColumnName("CNPJ").IsFixedLength().HasMaxLength(14).IsRequired();
            Property(e => e.Codigo).HasColumnName("Codigo").IsRequired();
            Property(e => e.Email).HasColumnName("Email").IsRequired();
            Property(e => e.TipoRepresentante).HasColumnName("TipoRepresentante").IsRequired();

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();
        }
    }
}