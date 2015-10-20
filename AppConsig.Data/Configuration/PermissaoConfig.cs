using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class PermissaoConfig : EntityTypeConfiguration<Permissao>
    {
        public PermissaoConfig()
        {
            ToTable("Permissao");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Descricao).HasColumnName("Descricao").IsOptional();
            Property(e => e.Url).HasColumnName("Url").IsOptional();
            Property(e => e.Acao).HasColumnName("Acao").IsOptional();
            Property(e => e.Controle).HasColumnName("Controle").IsOptional();
            Property(e => e.ClasseIcone).HasColumnName("ClasseIcone").IsOptional();
            Property(e => e.ParenteId).HasColumnName("ParenteId").IsOptional();
            Property(e => e.Ordem).HasColumnName("Ordem").IsRequired();
            Property(e => e.EhPadrao).HasColumnName("EhPadrao").IsRequired();
            Property(e => e.MostrarNoMenu).HasColumnName("MostrarNoMenu").IsRequired();
            Property(e => e.EhCRUD).HasColumnName("EhCRUD").IsRequired();
            Property(e => e.Atributos).HasColumnName("Atributos").IsOptional();
        }
    }
}