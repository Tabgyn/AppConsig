using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Comum;
using AppConsig.Dados;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoEntidade<T> : IServicoEntidade<T> where T : EntidadeBase
    {
        protected IContexto _Contexto;
        protected IDbSet<T> _Dbset;

        public ServicoEntidade(IContexto contexto)
        {
            _Contexto = contexto;
            _Dbset = contexto.Set<T>();
        }

        public virtual void Criar(T entidade)
        {
            if (entidade == null)
            {
                throw new ArgumentNullException("entidade");
            }

            _Dbset.Add(entidade);
            _Contexto.SaveChanges();
        }

        public virtual void Deletar(T entidade)
        {
            if (entidade == null) throw new ArgumentNullException("entidade");

            _Dbset.Remove(entidade);
            _Contexto.SaveChanges();  
        }

        public virtual IEnumerable<T> ObterTodos()
        {
            return _Dbset.AsEnumerable(); 
        }

        public virtual void Atualizar(T entidade)
        {
            if (entidade == null) throw new ArgumentNullException("entidade");

            _Contexto.Entry(entidade).State = EntityState.Modified;
            _Contexto.SaveChanges();
        }
    }
}