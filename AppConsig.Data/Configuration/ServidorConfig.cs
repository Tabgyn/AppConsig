using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class ServidorConfig : EntityTypeConfiguration<Servidor>
    {
        public ServidorConfig()
        {
            ToTable("Servidor");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.CPF).HasColumnName("CPF").IsRequired();
            Property(e => e.Matricula).HasColumnName("Matricula").IsRequired();
            Property(e => e.NascidoEm).HasColumnName("NascidoEm").IsOptional();
            Property(e => e.Foto).HasColumnName("Foto").IsOptional();
            Property(e => e.AdmitidoEm).HasColumnName("AdmitidoEm").IsOptional();
            Property(e => e.AfastadoEm).HasColumnName("AfastadoEm").IsOptional();
            
            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();

            HasRequired(u => u.Departamento)
                .WithMany()
                .HasForeignKey(u => u.DepartamentoId);
        }
    }
}