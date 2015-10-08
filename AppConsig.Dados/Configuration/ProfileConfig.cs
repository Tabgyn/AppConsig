using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class ProfileConfig : EntityTypeConfiguration<Profile>
    {
        public ProfileConfig()
        {
            ToTable("Perfil");

            HasKey(e => e.Id);

            Property(e => e.Name).HasColumnName("Nome").IsRequired();
            Property(e => e.Description).HasColumnName("Descricao");
            Property(e => e.IsEditable).HasColumnName("Editavel").IsRequired();

            //IsAuditable
            Property(e => e.CreateBy).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CreateDate).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.UpdateBy).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.UpdateDate).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Deleted).HasColumnName("Excluido").IsRequired();

            HasMany(u => u.Permissions)
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