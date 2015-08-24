using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    public class Auditoria : Entidade<Guid>
    {
        [ScaffoldColumn(false)]
        public long UsuarioId { get; set; }

        [ScaffoldColumn(false)]
        public string SessionId { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Acao { get; set; }

        public string Controle { get; set; }
        
        public string NomeTabela { get; set; }

        [ScaffoldColumn(false)]
        public long RegistroId { get; set; }

        public string ValorOriginal { get; set; }

        public string ValorNovo { get; set; }
    }
}