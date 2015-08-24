namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1645 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perfil", "Editavel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Perfil", "Editavel");
        }
    }
}
