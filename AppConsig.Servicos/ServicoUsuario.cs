using System.Linq;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoUsuario : ServicoEntidade<Usuario>, IServicoUsuario
    {
        public ServicoUsuario(IContexto contexto)
            : base(contexto)
        {
            _Contexto = contexto;
            _Dbset = _Contexto.Set<Usuario>();
        }

        public override void Criar(Usuario entidade)
        {
            //Todo Setar senha criptografada

            base.Criar(entidade);
        }

        public Usuario ObterPeloId(long id)
        {
            return _Dbset.FirstOrDefault(x => x.Id == id);
        }

        public bool ValidarUsuario(string login, string senha)
        {
            var usuario = _Dbset.FirstOrDefault(x => x.Login == login);

            if (usuario == null)
            {
                return false;
            }

            if (usuario.Senha != senha)
            {
                return false;
            }

            return true;
        }
    }
}