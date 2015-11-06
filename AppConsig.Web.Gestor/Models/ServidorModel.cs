using System.ComponentModel.DataAnnotations;

namespace AppConsig.Web.Gestor.Models
{
    public class ServidorListModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        [Display(Name = @"Matrícula")]
        public string Matricula { get; set; }
    }

    public class ServidorEditModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        [Display(Name = @"Matrícula")]
        public string Matricula { get; set; }

        [Display(Name = @"Nascido em")]
        public string NascidoEm { get; set; }

        public string Foto { get; set; }

        [Display(Name = @"Admitido em")]
        public string AdmitidoEm { get; set; }

        [Display(Name = @"Afastado em")]
        public string AfastadoEm { get; set; }
        
        public string Departamento { get; set; }
    }
}