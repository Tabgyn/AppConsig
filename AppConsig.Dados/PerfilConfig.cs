using System.Data.Entity.ModelConfiguration;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public class PerfilConfig : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfig()
        {
            HasMany(u => u.Permissoes)
                .WithMany(p => p.Perfis)
                .Map(up =>
                     {
                         up.MapLeftKey("PerfilId");
                         up.MapRightKey("PermissaoId");
                         up.ToTable("PerfilPermissao");
                     });
        }
    }
}