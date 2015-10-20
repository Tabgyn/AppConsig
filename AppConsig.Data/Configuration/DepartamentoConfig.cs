using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class DepartamentoConfig : EntityTypeConfiguration<Departamento>
    {
        public DepartamentoConfig()
        {
            ToTable("Departamento");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Descricao).HasColumnName("Descricao").IsOptional();
            Property(e => e.CodigoDepartamento).HasColumnName("CodigoDepartamento").IsRequired();
            Property(e => e.SistemaFolhaId).HasColumnName("SistemaFolhaId").IsRequired();

            //IsAuditable
            Property(e => e.CreateBy).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CreateDate).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.UpdateBy).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.UpdateDate).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Deleted).HasColumnName("Excluido").IsRequired();
            
            HasRequired(u => u.SistemaFolha)
                .WithMany()
                .HasForeignKey(u => u.SistemaFolhaId);
        }
    }
}