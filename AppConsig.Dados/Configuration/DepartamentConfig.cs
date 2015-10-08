using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class DepartamentConfig : EntityTypeConfiguration<Department>
    {
        public DepartamentConfig()
        {
            ToTable("Departamento");

            HasKey(e => e.Id);

            Property(e => e.Name).HasColumnName("Nome").IsRequired();
            Property(e => e.Description).HasColumnName("Descricao").IsRequired();
            Property(e => e.DepartmentCode).HasColumnName("Codigo").IsRequired();

            //IsAuditable
            Property(e => e.CreateBy).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CreateDate).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.UpdateBy).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.UpdateDate).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Deleted).HasColumnName("Excluido").IsRequired();

            HasRequired(u => u.HumanResourceSystem)
                .WithMany()
                .HasForeignKey(u => u.HumanResourceSystemId);
        }
    }
}