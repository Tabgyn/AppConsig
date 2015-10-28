using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class PerfilConfig : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfig()
        {
            ToTable("Perfil");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Descricao).HasColumnName("Descricao").IsOptional();
            Property(e => e.EhEditavel).HasColumnName("EhEditavel").IsRequired();

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();

            HasMany(u => u.Permissoes)
                .WithMany()
                .Map(up =>
                     {
                         up.MapLeftKey("PerfilId");
                         up.MapRightKey("PermissaoId");
                         up.ToTable("PerfilPermissao");
                     });
        }
    }
}