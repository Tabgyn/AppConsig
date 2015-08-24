namespace AppConsig.Web.Base.Entidades
{
    public class Alerta
    {
        public const string TempDataKey = "TempDataAlertas";
        public string EstiloAlerta { get; set; }
        public string EstiloIcone { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public bool Descartavel { get; set; }
        public string Exception { get; set; }
    }

    public static class EstiloAlerta
    {
        public const string Successo = "success";
        public const string Informacao = "info";
        public const string Atencao = "warning";
        public const string Erro = "danger";
    }
}