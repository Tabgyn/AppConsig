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
            Property(e => e.Endereco).HasColumnName("Endereco").IsOptional();
            Property(e => e.EnderecoComplemento).HasColumnName("EnderecoComplemento").IsOptional();
            Property(e => e.Foto).HasColumnName("Foto").IsOptional();
            Property(e => e.UltimoAcesso).HasColumnName("UltimoAcesso").IsOptional();
            Property(e => e.Bloqueado).HasColumnName("Bloqueado").IsOptional();
            Property(e => e.Telefone).HasColumnName("Telefone").IsOptional();
            Property(e => e.Celular).HasColumnName("Celular").IsOptional();
            Property(e => e.Facebook).HasColumnName("Facebook").IsOptional();
            Property(e => e.Twitter).HasColumnName("Twitter").IsOptional();
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