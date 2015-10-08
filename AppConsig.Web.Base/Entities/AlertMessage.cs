namespace AppConsig.Web.Base.Entities
{
    public class AlertMessage
    {
        public const string TempDataKey = "TempDataAlerts";
        public string AlertMessageStyle { get; set; }
        public string IconClass { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Disposable { get; set; }
        public string Exception { get; set; }
    }

    public static class AlertMessageStyle
    {
        public const string Success = "success";
        public const string Info = "info";
        public const string Warning = "warning";
        public const string Erro = "danger";
    }
}