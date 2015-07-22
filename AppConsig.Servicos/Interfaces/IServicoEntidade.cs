using System.Collections.Generic;
using AppConsig.Comum;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoEntidade<T> : IServico where T : EntidadeBase
    {
        void Criar(T entity);
        void Deletar(T entity);
        IEnumerable<T> ObterTodos();
        void Atualizar(T entity); 
    }
}