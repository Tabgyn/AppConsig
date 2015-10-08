using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("Usuario");

            HasKey(e => e.Id);

            Property(e => e.Name).HasColumnName("Nome").IsRequired();
            Property(e => e.Surname).HasColumnName("Sobrenome").IsRequired();
            Property(e => e.Email).HasColumnName("Email").IsRequired();
            Property(e => e.Login).HasColumnName("Login").IsRequired();
            Property(e => e.Password).HasColumnName("Senha").IsRequired();
            Property(e => e.IsAdmin).HasColumnName("Admin").IsRequired();
            Property(e => e.Address).HasColumnName("Endereco");
            Property(e => e.ComplementAddress).HasColumnName("EnderecoComplemento");
            Property(e => e.Picture).HasColumnName("Foto");
            Property(e => e.PhoneNumber).HasColumnName("Telefone");
            Property(e => e.MobileNumber).HasColumnName("Celular");
            Property(e => e.Facebook).HasColumnName("Facebook");
            Property(e => e.Twitter).HasColumnName("Twitter");

            //IsAuditable
            Property(e => e.CreateBy).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CreateDate).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.UpdateBy).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.UpdateDate).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Deleted).HasColumnName("Excluido").IsRequired();

            HasRequired(u => u.Profile)
                .WithMany()
                .HasForeignKey(u => u.ProfileId);
        }
    }
}