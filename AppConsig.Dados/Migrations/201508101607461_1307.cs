namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1307 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Aviso", "CriadoPor", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Aviso", "AtualizadoPor", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Perfil", "CriadoPor", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Perfil", "AtualizadoPor", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Usuario", "Sobrenome", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "CriadoPor", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Usuario", "AtualizadoPor", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "AtualizadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "CriadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "Sobrenome", c => c.String());
            AlterColumn("dbo.Perfil", "AtualizadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Perfil", "CriadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Aviso", "AtualizadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Aviso", "CriadoPor", c => c.String(nullable: false));
        }
    }
}
