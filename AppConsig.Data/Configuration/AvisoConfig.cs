﻿using System.Data.Entity.ModelConfiguration;
using AppConsig.Entities;

namespace AppConsig.Data.Configuration
{
    public class AvisoConfig : EntityTypeConfiguration<Aviso>
    {
        public AvisoConfig()
        {
            ToTable("Aviso");

            HasKey(e => e.Id);

            Property(e => e.Texto).HasColumnName("Texto").IsRequired();

            //IsAuditable
            Property(e => e.CriadoPor).HasColumnName("CriadoPor").IsRequired();
            Property(e => e.CriadoEm).HasColumnName("CriadoEm").IsRequired();
            Property(e => e.AtualizadoPor).HasColumnName("AtualizadoPor").IsRequired();
            Property(e => e.AtualizadoEm).HasColumnName("AtualizadoEm").IsRequired();
            Property(e => e.Excluido).HasColumnName("Excluido").IsRequired();
        }
    }
}