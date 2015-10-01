using System;
using System.Collections.Generic;
using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoUsuario : IServicoEntidade<Usuario>
    {
        bool ValidarUsuario(string login, string senha);
        void ReeviarSenha(Usuario usuario);
        List<Permissao> ObterPermissoesDoUsuario(Guid usuarioId);
    }
}