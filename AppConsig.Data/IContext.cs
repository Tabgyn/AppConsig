﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using AppConsig.Entities;

namespace AppConsig.Data
{
    public interface IContext
    {
        IDbSet<Auditoria> Auditorias { get; set; }
        IDbSet<Aviso> Avisos { get; set; }
        IDbSet<Departamento> Departamentos { get; set; }
        IDbSet<Perfil> Perfis { get; set; }
        IDbSet<Permissao> Permissoes { get; set; }
        IDbSet<SistemaFolha> SistemasFolha { get; set; }
        IDbSet<Usuario> Usuarios { get; set; }
        IDbSet<Consignacao> Consignacoes { get; set; }
        IDbSet<Consignataria> Consignatarias { get; set; }
        IDbSet<Servico> Servicos { get; set; }
        IDbSet<Servidor> Servidores { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}