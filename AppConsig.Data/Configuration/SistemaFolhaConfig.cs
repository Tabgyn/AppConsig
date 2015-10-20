using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class SistemaFolhaConfig : EntityTypeConfiguration<SistemaFolha>
    {
        public SistemaFolhaConfig()
        {
            ToTable("SistemaFolha");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
        }
    }
}