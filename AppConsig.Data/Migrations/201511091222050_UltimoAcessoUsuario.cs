namespace AppConsig.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UltimoAcessoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "UltimoAcesso", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "UltimoAcesso");
        }
    }
}
