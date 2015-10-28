using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable("Usuario");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Sobrenome).HasColumnName("Sobrenome").IsRequired();
            Property(e => e.Email).HasColumnName("Email").IsRequired();
            Property(e => e.NomeDeUsuario).HasColumnName("NomeDeUsuario").IsRequired();
            Property(e => e.Senha).HasColumnName("Senha").IsRequired();
            Property(e => e.EhAdministrador).HasColumnName("EhAdministrador").IsRequired();
            Property(e => e.Endereco).HasColumnName("Endereco");
            Property(e => e.EnderecoComplemento).HasColumnName("EnderecoComplemento");
            Property(e => e.Foto).HasColumnName("Foto");
            Property(e => e.Telefone).HasColumnName("Telefone");
            Property(e => e.Celular).HasColumnName("Celular");
            Property(e => e.Facebook).HasColumnName("Facebook");
            Property(e => e.Twitter).HasColumnName("Twitter");
            Property(e => e.PerfilId).HasColumnName("PerfilId");

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();

            HasRequired(u => u.Perfil)
                .WithMany()
                .HasForeignKey(u => u.PerfilId);
        }
    }
}