using AppConsig.Comum.Interfaces;

namespace AppConsig.Comum
{
    public abstract class EntidadeBase
    {
        
    }

    public abstract class Entidade<T> : EntidadeBase, IEntidade<T>
    {
        public virtual T Id { get; set; }
    }
}