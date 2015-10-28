using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class ServicoConfig : EntityTypeConfiguration<Servico>
    {
        public ServicoConfig()
        {
            ToTable("Servico");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Descricao).HasColumnName("Descricao").IsOptional();
            Property(e => e.TipoServicoRelacao).HasColumnName("TipoServicoRelacao").IsRequired();
            Property(e => e.TipoServicoInerente).HasColumnName("TipoServicoInerente").IsRequired();
            Property(e => e.Ordem).HasColumnName("Ordem").IsRequired();

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();
        }
    }
}