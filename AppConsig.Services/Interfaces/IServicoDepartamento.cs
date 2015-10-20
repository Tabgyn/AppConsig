using System.Collections.Generic;
using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IServicoDepartamento : IServicoBasico<Departamento>
    {
        List<SistemaFolha> ObterSistemasFolha();
    }
}