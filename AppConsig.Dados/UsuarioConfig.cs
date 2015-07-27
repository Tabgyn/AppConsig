using System.Data.Entity.ModelConfiguration;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasRequired(u => u.Perfil)
                .WithMany()
                .HasForeignKey(u => u.PerfilId);
        }
    }
}