namespace AppConsig.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioBloqueado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Bloqueado", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Bloqueado");
        }
    }
}
