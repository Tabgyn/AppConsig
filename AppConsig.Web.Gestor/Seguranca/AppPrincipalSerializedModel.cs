using System.Collections.Generic;
using AppConsig.Entidades;

namespace AppConsig.Web.Gestor.Seguranca
{
    public class AppPrincipalSerializedModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public ICollection<Permissao> Permissoes { get; set; }
    }
}