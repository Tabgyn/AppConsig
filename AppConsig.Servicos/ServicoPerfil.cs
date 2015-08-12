using System.Collections.Generic;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoPerfil : ServicoEntidade<Perfil>, IServicoPerfil
    {
        public ServicoPerfil(IContexto contexto) 
            : base(contexto) 
        {
            Contexto = contexto;
            Dbset = Contexto.Set<Perfil>();
        }
    }
}