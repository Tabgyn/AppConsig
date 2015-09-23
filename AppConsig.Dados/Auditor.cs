using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AppConsig.Entidades;

namespace AppConsig.Dados
{
    public class Auditor : IDisposable
    {
        private readonly DbEntityEntry _entrada;

        public Auditor(DbEntityEntry entrada)
        {
            _entrada = entrada;
        }

        public Auditoria CriarAuditoria(object nomeUsuario, TipoEvento tipoEvento, AppContexto context)
        {
            var entityType = _entrada.Entity.GetType().GetEntityType();
            var dataAtual = DateTime.Now;
            var keyNames = entityType.GetPrimaryKeyNames(context);

            var auditoria = new Auditoria
            {
                TipoEvento = tipoEvento,
                DataEvento = dataAtual,
                Usuario = nomeUsuario?.ToString(),
                NomeEntidade = entityType.FullName,
                RegistroId = _entrada.GetPrimaryKeyValues(keyNames).ToString(),
                DetalhesAuditoria = CriarDetalhesAuditoria()
            };

            return auditoria;
        }

        private ICollection<DetalheAuditoria> CriarDetalhesAuditoria()
        {
            var type = _entrada.Entity.GetType().GetEntityType();

            return PropertyNamesOf(_entrada).Select(propertyName => new DetalheAuditoria
            {
                Propriedade = type.GetPropertyName(propertyName),
                ValorOriginal = OriginalValue(propertyName),
                ValorNovo = CurrentValue(propertyName),
            }).ToList();
        }

        protected virtual EntityState StateOf(DbEntityEntry dbEntry)
        {
            return dbEntry.State;
        }

        private IEnumerable<string> PropertyNamesOf(DbEntityEntry dbEntry)
        {
            var propertyValues = (StateOf(dbEntry) == EntityState.Added)
                ? dbEntry.CurrentValues
                : dbEntry.OriginalValues;

            return propertyValues.PropertyNames;
        }

        private string OriginalValue(string propertyName)
        {
            if (StateOf(_entrada) == EntityState.Added)
            {
                return null;
            }

            var value = _entrada.Property(propertyName).OriginalValue;

            return value?.ToString();
        }

        private string CurrentValue(string propertyName)
        {
            if (StateOf(_entrada) == EntityState.Deleted)
            {
                return null;
            }

            var value = _entrada.Property(propertyName).CurrentValue;

            return value?.ToString();
        }

        public void Dispose()
        {

        }
    }
}