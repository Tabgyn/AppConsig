namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201507221550 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Nome", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Usuario", "Sobrenome", c => c.String());
            AlterColumn("dbo.Usuario", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Email", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Usuario", "Sobrenome");
            DropColumn("dbo.Usuario", "Nome");
        }
    }
}
