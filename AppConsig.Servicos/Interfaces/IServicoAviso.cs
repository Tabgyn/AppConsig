using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoAviso : IServicoEntidade<Aviso>
    {
        Aviso ObterPeloId(long id);
    }
}