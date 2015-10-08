using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class HumanResourceSystemConfig : EntityTypeConfiguration<HumanResourceSystem>
    {
        public HumanResourceSystemConfig()
        {
            ToTable("SistemaFolha");

            HasKey(e => e.Id);

            Property(e => e.Name).HasColumnName("Nome").IsRequired();
        }
    }
}