namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0957 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissao", "Visivel", c => c.Boolean(nullable: false));
            DropColumn("dbo.Permissao", "VisivelNoMenu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissao", "VisivelNoMenu", c => c.Boolean(nullable: false));
            DropColumn("dbo.Permissao", "Visivel");
        }
    }
}
