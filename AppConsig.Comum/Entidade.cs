using System;
using AppConsig.Comum.Interfaces;

namespace AppConsig.Comum
{
    public abstract class EntidadeBase
    {
        
    }

    public class Entidade : EntidadeBase, IEntidade
    {
        public Guid Id { get; set; }
    }
}