namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1632 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissao", "IsCrud", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissao", "Atributos", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissao", "Atributos");
            DropColumn("dbo.Permissao", "IsCrud");
        }
    }
}
