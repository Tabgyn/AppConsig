﻿using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class DepartamentoConfig : EntityTypeConfiguration<Departamento>
    {
        public DepartamentoConfig()
        {
            ToTable("Departamento");

            HasKey(e => e.Id);

            Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            Property(e => e.Descricao).HasColumnName("Descricao").IsOptional();
            Property(e => e.CodigoDepartamento).HasColumnName("CodigoDepartamento").IsRequired();
            Property(e => e.SistemaFolhaId).HasColumnName("SistemaFolhaId").IsRequired();

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();
            
            HasRequired(u => u.SistemaFolha)
                .WithMany()
                .HasForeignKey(u => u.SistemaFolhaId);
        }
    }
}