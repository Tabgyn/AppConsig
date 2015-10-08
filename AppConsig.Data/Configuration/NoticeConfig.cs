using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class NoticeConfig : EntityTypeConfiguration<Notice>
    {
        public NoticeConfig()
        {
            ToTable("Aviso");

            HasKey(e => e.Id);

            Property(e => e.Text).HasColumnName("Texto").IsRequired();

            //IsAuditable
            Property(e => e.CreateBy).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CreateDate).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.UpdateBy).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.UpdateDate).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Deleted).HasColumnName("Excluido").IsRequired();
        }
    }
}