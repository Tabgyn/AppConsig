namespace AppConsig.Common.Security
{
    public class AppPrincipalSerializedModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}