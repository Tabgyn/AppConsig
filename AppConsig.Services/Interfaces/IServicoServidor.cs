using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IServicoServidor : IServicoBasico<Servidor>
    {
        Servidor ObterServidorComDepartamento(long id);
    }
}