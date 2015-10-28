using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class ConsignacaoConfig : EntityTypeConfiguration<Consignacao>
    {
        public ConsignacaoConfig()
        {
            ToTable("Consignacao");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Descricao).HasColumnName("Descricao").IsOptional();
            Property(e => e.Codigo).HasColumnName("Codigo").IsRequired();
            Property(e => e.MaximoParcela).HasColumnName("MaximoParcela").IsRequired();
            Property(e => e.ValorMinimo).HasColumnName("ValorMinimo").IsRequired();
            Property(e => e.InicioDaVigenciaEm).HasColumnName("InicioDaVigenciaEm").IsRequired();
            Property(e => e.FimDaVigenciaEm).HasColumnName("FimDaVigenciaEm").IsOptional();
            Property(e => e.PermiteDescontoParcial).HasColumnName("PermiteDescontoParcial").IsRequired();
            Property(e => e.PermiteLancamentoManual).HasColumnName("PermiteLancamentoManual").IsRequired();
            Property(e => e.PermiteOutrasOcorrencias).HasColumnName("PermiteOutrasOcorrencias").IsRequired();

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();

            HasRequired(u => u.Servico)
                .WithMany()
                .HasForeignKey(u => u.ServicoId);

            HasRequired(u => u.Consignataria)
                .WithMany()
                .HasForeignKey(u => u.ConsignatariaId);
        }
    }
}