using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class PermissionConfig : EntityTypeConfiguration<Permission>
    {
        public PermissionConfig()
        {
            ToTable("Permissao");

            HasKey(e => e.Id);

            Property(e => e.Name).HasColumnName("Nome").IsRequired();
            Property(e => e.Description).HasColumnName("Descricao").IsOptional();
            Property(e => e.Url).HasColumnName("Url").IsOptional();
            Property(e => e.Action).HasColumnName("Action").IsOptional();
            Property(e => e.Controller).HasColumnName("Controller").IsOptional();
            Property(e => e.IconClass).HasColumnName("Icone").IsOptional();
            Property(e => e.ParentId).HasColumnName("ParenteId").IsOptional();
            Property(e => e.Order).HasColumnName("Ordem").IsRequired();
            Property(e => e.IsStandard).HasColumnName("Padrao").IsRequired();
            Property(e => e.ShowInMenu).HasColumnName("MostrarNoMenu").IsRequired();
            Property(e => e.IsCrud).HasColumnName("Crud").IsRequired();
            Property(e => e.Attributes).HasColumnName("Atributos").IsOptional();
        }
    }
}